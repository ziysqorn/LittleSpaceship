using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

public class QuizDatabase
{
    protected Dictionary<string, List<QuizObject>> quizMap = new Dictionary<string, List<QuizObject>>();
	protected QuizObject currentQuiz;
	protected int currentLevel;
	protected HashSet<string> askedQuestionSet = new HashSet<string>();
    public QuizDatabase()
    {
		TextAsset quizFile = Resources.Load<TextAsset>("QuizDatabase");
		string textContent = quizFile.text;
		if (textContent != "")
		{
			JArray jArray = JArray.Parse(textContent);
			foreach (JObject quiz in jArray)
			{
				string quizTopic = (string)quiz["topic"];
				int quizLevel = (int)quiz["level"];
				string quizQuestion = (string)quiz["question"];
				string quizRightAnswer = (string)quiz["rightAnswer"];
				QuizObject quizObj = new QuizObject(quizTopic, quizLevel, quizQuestion, quizRightAnswer, quiz["answers"].ToObject<List<string>>());
				AddQuiz(quizObj.topic, quizObj);
			}
			QuizHistory quizHistory = GameplayStatics.LoadGame<QuizHistory>("QuizHistory.space");
			if(quizHistory != null)
			{
				currentLevel = quizHistory.currentLevel;
				foreach(string quizQuestion in quizHistory.completedQuiz)
				{
					if (!askedQuestionSet.Contains(quizQuestion))
						askedQuestionSet.Add(quizQuestion);
				}
			}
			else
			{
				quizHistory = new QuizHistory();
				GameplayStatics.SaveGame(quizHistory, "QuizHistory.space");
			}
		}
	}

    public void AddQuiz(in string topic, in QuizObject quiz)
    {
        if (!quizMap.ContainsKey(topic))
        {
            List<QuizObject> list = new List<QuizObject>();
            list.Add(quiz); 
            quizMap.Add(topic, list);
        }
        else quizMap[topic].Add(quiz);
    }

	public QuizObject ChooseRandom(in string topic)
	{
		if (quizMap.ContainsKey(topic))
		{
			int randomQuiz = Random.Range(0, quizMap[topic].Count);
			if (!askedQuestionSet.Contains(quizMap[topic][randomQuiz].question))
			{
				currentQuiz = quizMap[topic][randomQuiz];
				return quizMap[topic][randomQuiz];
			}
		}
		currentQuiz = null;
		return null;
	}

	public bool SubmitAnswer(in string inAnswer)
	{
		if(currentQuiz != null && currentQuiz.rightAnswer == inAnswer)
		{
			askedQuestionSet.Add(currentQuiz.question);
			QuizHistory quizHistory = GameplayStatics.LoadGame<QuizHistory>("QuizHistory.space");
			if (quizHistory != null) { 
				quizHistory.completedQuiz.Add(currentQuiz.question);
				GameplayStatics.SaveGame(quizHistory, "QuizHistory.space");
			}
			currentQuiz = null;
			return true;
		}
		return false;
	}
}
