using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] protected TextMeshProUGUI txt_HighestScore;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
		if (scoreboardSave == null)
		{
			scoreboardSave = new ScoreboardSave();
			scoreboardSave.curScore = 0;
			scoreboardSave.maxScore = 0;
		}
		if (txt_HighestScore)
			txt_HighestScore.text = scoreboardSave.maxScore.ToString();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
		if (scoreboardSave != null) scoreboardSave.curScore = 0;
        else
        {
			scoreboardSave = new ScoreboardSave();
            scoreboardSave.curScore = 0;
            scoreboardSave.maxScore = 0;
		}
        GameplayStatics.DeleteSaveGame("GameProgress.space");
		GameplayStatics.DeleteSaveGame("PlayerInfo.space");
		GameplayStatics.DeleteSaveGame("QuizHistory.space");
		GameplayStatics.DeleteSaveGame("SavedCurrentWeapon.space");
		GameplayStatics.SaveGame(scoreboardSave, "ScoreboardSave.space");
        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
		GameProgress gameProgress = GameplayStatics.LoadGame<GameProgress>("GameProgress.space");
        if (gameProgress != null) {
			SceneManager.LoadScene("MainScene");
		}
	}

	public void Quit()
	{
        Application.Quit();
	}
}
