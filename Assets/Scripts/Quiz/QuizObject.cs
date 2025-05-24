using System.Collections.Generic;
using UnityEngine;

public class QuizObject
{
    public string topic;
	public string question { get; private set; }
	public string rightAnswer { get; private set; }
	public List<string> answers { get; private set; }

	public QuizObject()
	{

	}
    public QuizObject(in string topic, in string question, in string rightAnswer, List<string> answers)
	{
		this.topic = topic;
		this.question = question;
		this.rightAnswer = rightAnswer;
		this.answers = answers;
	} 
}
