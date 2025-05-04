using System.Collections.Generic;
using UnityEngine;

public class QuizObject
{
    public string topic;
	protected int level { get; private set; }
    protected string question { get; private set; }
    protected string rightAnswer { get; private set; }
	protected List<string> answers { get; private set; }

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
