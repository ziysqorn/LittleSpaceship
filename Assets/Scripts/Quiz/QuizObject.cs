using System.Collections.Generic;
using UnityEngine;

public class QuizObject
{
    public string topic;
	public int level { get; private set; }
	public string question { get; private set; }
	public string rightAnswer { get; private set; }
	public List<string> answers { get; private set; }

	public QuizObject()
	{

	}
    public QuizObject(in string topic, in int level, in string question, in string rightAnswer, List<string> answers)
	{
		this.topic = topic;
		this.level = level;
		this.question = question;
		this.rightAnswer = rightAnswer;
		this.answers = answers;
	} 
}
