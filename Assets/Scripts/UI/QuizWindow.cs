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
    public float remainingTime { get; private set; } = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatabaseManager dbManager = DatabaseManager.instance;
        if (dbManager)
        {
            QuizDatabase quizDb = dbManager.quizDb;
            if (quizDb != null)
            {
                QuizObject quizObj = quizDb.ChooseRandom("Math");
                if (quizObj != null) {
                    if (quizQuestionText)
                    {
                        quizQuestionText.text = "Question: " + quizObj.question;
                    }
                    for(int i = 0; i < toggleContainer.transform.childCount; ++i)
                    {
                        Toggle toggleAnswer = toggleContainer.transform.GetChild(i).GetComponent<Toggle>();
                        if (toggleAnswer != null) {
							char choiceLetter = (char)('A' + i);
							TextMeshProUGUI toggleContent = toggleAnswer.GetComponentInChildren<TextMeshProUGUI>();
							if (toggleContent != null)
							{
								toggleContent.text = choiceLetter + ". " + quizObj.answers[i];
							}
						}
                    }
				}
            }
        }
        Time.timeScale = 0.0f;
        Cursor.visible = true;
		if (quizTimerText) quizTimerText.text = Mathf.CeilToInt(remainingTime).ToString();
		StartCoroutine(QuizTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void EndQuiz()
    {
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		Destroy(gameObject);
    }

    public void setRemainingTime(in float inTime)
    {
        remainingTime = inTime;
    }
}
