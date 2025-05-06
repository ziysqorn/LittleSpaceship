using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizHistory : SaveObject
{
    public List<string> completedQuiz;
    public int currentLevel;

    public QuizHistory()
    {
        completedQuiz = new List<string>();
        currentLevel = 1;
    }
}
