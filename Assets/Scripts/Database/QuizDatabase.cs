using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

public class QuizDatabase
{
    protected Dictionary<string, List<QuizObject>> quizMap = new Dictionary<string, List<QuizObject>>();
	protected List<QuizObject> quizList = new List<QuizObject>();
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
				string quizQuestion = (string)quiz["question"];
				string quizRightAnswer = (string)quiz["rightAnswer"];
				QuizObject quizObj = new QuizObject(quizTopic, quizQuestion, quizRightAnswer, quiz["answers"].ToObject<List<string>>());
				AddQuiz(quizObj.topic, quizObj);
				quizList.Add(quizObj);
			}
			QuizHistory quizHistory = GameplayStatics.LoadGame<QuizHistory>("QuizHistory.space");
			if(quizHistory != null)
			{
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

	public List<KeyValuePair<QuizObject, string>> getRandomQuizList(int quizCount)
	{
		List<KeyValuePair<QuizObject, string>> list = new List<KeyValuePair<QuizObject, string>>();
		if (quizCount > 0)
		{
			int randomQuiz = Random.Range(0, quizList.Count);
			int direction = Random.Range(0, 2);
			if (direction == 0)
			{
				for (int i = 0; i < quizCount; ++i)
				{
					int idx = (randomQuiz + quizList.Count) % quizList.Count;
					list.Add(new KeyValuePair<QuizObject, string>(quizList[idx], ""));
					++randomQuiz;
				}
			}
			else
			{
				for (int i = 0; i < quizCount; ++i)
				{
					int idx = randomQuiz % quizList.Count;
					list.Add(new KeyValuePair<QuizObject, string>(quizList[idx], ""));
					++randomQuiz;
				}
			}
		}
		return list;
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
