using System;
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
    protected List<KeyValuePair<QuizObject, string>> quizList;
    protected int currentQuizIdx = 0;
    protected MainCharacter owner;
	public event Action<int, int> OnQuizEnd;
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
		QuizObject quizObj = quizList[currentIdx].Key;
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
		if (btn_Summit)
		{
			btn_Summit.GetComponent<Button>().enabled = false;
			btn_Summit.GetComponent<Image>().enabled = false;
			btn_Summit.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
		}
		if(currentIdx == 0)
		{
			if (btn_prevQuestion)
			{
				btn_prevQuestion.GetComponent<Button>().enabled = false;
				btn_prevQuestion.GetComponent<Image>().enabled = false;
				btn_prevQuestion.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
			}
		}
		else
		{
			if (btn_prevQuestion)
			{
				btn_prevQuestion.GetComponent<Button>().enabled = true;
				btn_prevQuestion.GetComponent<Image>().enabled = true;
				btn_prevQuestion.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
			}
		}
		if(currentIdx == quizList.Count - 1)
		{
			if (btn_nextQuestion)
			{
				btn_nextQuestion.GetComponent<Button>().enabled = false;
				btn_nextQuestion.GetComponent<Image>().enabled = false;
				btn_nextQuestion.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
			}
			if (btn_Summit)
			{
				btn_Summit.GetComponent<Button>().enabled = true;
				btn_Summit.GetComponent<Image>().enabled = true;
				btn_Summit.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
			}
		}
		else
		{
			if (btn_nextQuestion)
			{
				btn_nextQuestion.GetComponent<Button>().enabled = true;
				btn_nextQuestion.GetComponent<Image>().enabled = true;
				btn_nextQuestion.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
			}
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

        EndQuiz();
    }

    public void setOwner(in MainCharacter inOwner)
    {
        owner = inOwner;
    }

	protected void updateAnswer()
	{
		if (answerGroup)
		{
			foreach (Toggle answer in answerGroup.ActiveToggles())
			{
				if (answer.isOn)
				{
					TextMeshProUGUI toggleLabel = answer.GetComponentInChildren<TextMeshProUGUI>();
					if (toggleLabel)
					{
						string realAnswer = toggleLabel.text.Remove(0, 3);
						realAnswer = realAnswer.TrimStart(' ');
						realAnswer = realAnswer.TrimEnd(' ');
						quizList[currentQuizIdx] = new KeyValuePair<QuizObject, string>(quizList[currentQuizIdx].Key, realAnswer);
					}
				}
			}
		}
	}

	protected void SubmitAnswer()
    {
		updateAnswer();
		for (int i = 0; i < quizList.Count; i++) {
			Debug.Log(quizList[i].Key + " - " + quizList[i].Value);
		}
		EndQuiz();
   //     if (answerGroup)
   //     {
   //         foreach(Toggle answer in answerGroup.ActiveToggles())
   //         {
			//	if (answer.isOn)
			//	{
   //                 TextMeshProUGUI toggleLabel = answer.GetComponentInChildren<TextMeshProUGUI>();
   //                 if (toggleLabel)
   //                 {
   //                     string realAnswer = toggleLabel.text.Remove(0, 3);
   //                     realAnswer = realAnswer.TrimStart(' ');
			//			realAnswer = realAnswer.TrimEnd(' ');
			//			DatabaseManager dbManager = DatabaseManager.instance;
			//			if (dbManager)
			//			{
			//				QuizDatabase quizDb = dbManager.quizDb;
			//				if (quizDb != null)
			//				{
   //                             string result = "";
   //                             bool bIsCorrect = false;
   //                             Color resultMessageColor;
   //                             if (quizDb.SubmitAnswer(realAnswer))
   //                             {
   //                                 result = "Correct";
   //                                 bIsCorrect = true;
			//						resultMessageColor = Color.green;
   //                             }
   //                             else
   //                             {
			//						result = "Wrong answer";
			//						bIsCorrect = false;
			//						resultMessageColor = Color.red;
			//					}
			//					StopCoroutine(QuizTimer());
   //                             EndQuiz(result, resultMessageColor, bIsCorrect);
			//				}
			//			}
			//		}
			//	}
			//}
   //     }
    }

	protected void PrevQuestion()
	{
		updateAnswer();
		--currentQuizIdx;
		fetchQuizInfo(currentQuizIdx);
	}

	protected void NextQuestion()
	{
		updateAnswer();
		++currentQuizIdx;
		fetchQuizInfo(currentQuizIdx);
	}



    public void EndQuiz()
    {
		int correctAnsCount = 0;
		for (int i = 0; i < quizList.Count; ++i)
		{
			if (quizList[i].Key.rightAnswer == quizList[i].Value) ++correctAnsCount;
		}
		if(OnQuizEnd != null) OnQuizEnd(correctAnsCount, quizList.Count);
		Destroy(gameObject);
    }

	public void setRemainingTime(in float inTime)
    {
        remainingTime = inTime;
    }
}
