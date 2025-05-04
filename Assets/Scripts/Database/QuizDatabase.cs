using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

public class QuizDatabase
{
    protected Dictionary<string, List<QuizObject>> quizMap = new Dictionary<string, List<QuizObject>>();
	protected HashSet<QuizObject> askedQuestion;
    public QuizDatabase()
    {
		TextAsset quizFile = Resources.Load<TextAsset>("QuizDatabase");
		string textContent = quizFile.text;
		if (textContent != "")
		{
			Debug.Log("Haha");
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

	public QuizObject ChooseRandom()
	{

		return null;
	}
}
