using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizHistory : SaveObject
{
    public List<string> completedQuiz;

    public QuizHistory()
    {
        completedQuiz = new List<string>();
    }
}
