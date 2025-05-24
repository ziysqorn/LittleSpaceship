using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizWindow : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI quizTimerText;
	[SerializeField] protected TextMeshProUGUI quizQuestionText;
    [SerializeField] protected GameObject toggleContainer;
    [SerializeField] protected Button btn_Summit;
	[SerializeField] protected Button btn_prevQuestion;
	[SerializeField] protected Button btn_nextQuestion;
	[SerializeField] protected ToggleGroup answerGroup;
    [SerializeField] protected PrefabData prefData;
    protected List<QuizObject> quizList;
    protected int currentQuizIdx = 0;
    protected MainCharacter owner;
    public float remainingTime { get; private set; } = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		if (btn_Summit) btn_Summit.onClick.AddListener(SubmitAnswer);
		if (btn_prevQuestion) btn_prevQuestion.onClick.AddListener(PrevQuestion);
		if (btn_nextQuestion) btn_nextQuestion.onClick.AddListener(NextQuestion);
		Time.timeScale = 0.0f;
        Cursor.visible = true;
		if (quizTimerText) quizTimerText.text = Mathf.CeilToInt(remainingTime).ToString();
		StartCoroutine(QuizTimer());
    }

    // Update is called once per frame
    void Update()
    {
	}

    public void initQuizList(int quizCount)
    {
		DatabaseManager dbManager = DatabaseManager.instance;
		if (dbManager)
		{
			QuizDatabase quizDb = dbManager.quizDb;
            quizList = quizDb?.getRandomQuizList(quizCount);
			fetchQuizInfo(currentQuizIdx);
		}
	}

    protected void fetchQuizInfo(int currentIdx)
    {
		QuizObject quizObj = quizList[currentIdx];
		if (quizObj != null)
		{
			if (quizQuestionText)
			{
				quizQuestionText.text = "Question: " + quizObj.question;
			}
			for (int i = 0; i < toggleContainer.transform.childCount; ++i)
			{
				Toggle toggleAnswer = toggleContainer.transform.GetChild(i).GetComponent<Toggle>();
				if (toggleAnswer != null)
				{
					char choiceLetter = (char)('A' + i);
					TextMeshProUGUI toggleContent = toggleAnswer.GetComponentInChildren<TextMeshProUGUI>();
					if (toggleContent != null)
					{
						toggleContent.text = choiceLetter + ". " + quizObj.answers[i];
					}
				}
			}
		}
		if(currentIdx == 0)
		{
			if (btn_prevQuestion) btn_prevQuestion.gameObject.SetActive(false);
		}
		else
		{
			if (btn_prevQuestion) btn_prevQuestion.gameObject.SetActive(true);
		}
		if(currentIdx == quizList.Count - 1)
		{
			if (btn_nextQuestion) btn_nextQuestion.gameObject.SetActive(false);
		}
		else
		{
			if (btn_nextQuestion) btn_nextQuestion.gameObject.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		if (btn_Summit)
		{
			btn_Summit.onClick.RemoveListener(SubmitAnswer);
		}
		if (btn_prevQuestion)
		{
			btn_prevQuestion.onClick.RemoveListener(PrevQuestion);
		}
		if (btn_nextQuestion)
		{
			btn_nextQuestion.onClick.RemoveListener(NextQuestion);
		}
	}

	protected IEnumerator QuizTimer()
    {
        while(remainingTime > 0.0f)
        {
			yield return null;
            remainingTime -= Time.unscaledDeltaTime;
            if(quizTimerText) quizTimerText.text = Mathf.CeilToInt(remainingTime).ToString();
		}

        EndQuiz("No answer submitted !", Color.white, false);
    }

    public void setOwner(in MainCharacter inOwner)
    {
        owner = inOwner;
    }

    protected void SubmitAnswer()
    {
        if (answerGroup)
        {
            foreach(Toggle answer in answerGroup.ActiveToggles())
            {
				if (answer.isOn)
				{
                    TextMeshProUGUI toggleLabel = answer.GetComponentInChildren<TextMeshProUGUI>();
                    if (toggleLabel)
                    {
                        string realAnswer = toggleLabel.text.Remove(0, 3);
                        realAnswer = realAnswer.TrimStart(' ');
						realAnswer = realAnswer.TrimEnd(' ');
						DatabaseManager dbManager = DatabaseManager.instance;
						if (dbManager)
						{
							QuizDatabase quizDb = dbManager.quizDb;
							if (quizDb != null)
							{
                                string result = "";
                                bool bIsCorrect = false;
                                Color resultMessageColor;
                                if (quizDb.SubmitAnswer(realAnswer))
                                {
                                    result = "Correct";
                                    bIsCorrect = true;
									resultMessageColor = Color.green;
                                }
                                else
                                {
									result = "Wrong answer";
									bIsCorrect = false;
									resultMessageColor = Color.red;
								}
								StopCoroutine(QuizTimer());
                                EndQuiz(result, resultMessageColor, bIsCorrect);
							}
						}
					}
				}
			}
        }
    }

	protected void PrevQuestion()
	{
		--currentQuizIdx;
		fetchQuizInfo(currentQuizIdx);
	}

	protected void NextQuestion()
	{
		++currentQuizIdx;
		fetchQuizInfo(currentQuizIdx);
	}



    public void EndQuiz(in string result, in Color resultMessageColor, in bool bIsCorrect)
    {
		if (prefData && prefData.screenMessagePref)
		{
			GameObject screenMessageUI = Instantiate(prefData.screenMessagePref);
			if (screenMessageUI != null)
			{
				ScreenMessageUI screenMessage = screenMessageUI.GetComponent<ScreenMessageUI>();
				if (screenMessage != null)
				{
					screenMessage.setScreenMessage(result);
					screenMessage.setMessageTextColor(resultMessageColor);
                    screenMessage.setCharacter(owner);
                    screenMessage.setResult(bIsCorrect);
				}
			}
		}
		Destroy(gameObject);
    }

    public void setRemainingTime(in float inTime)
    {
        remainingTime = inTime;
    }
}
