using GlobalAccess;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainMenu : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txt_HighestScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
		GameProgress gameProgress = new GameProgress();
		gameProgress.curWave = 0;
		MainSceneScript mainScene = FindFirstObjectByType<MainSceneScript>();
		ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
        PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
		if (mainScene != null && scoreboardSave != null)
		{
            scoreboardSave.maxScore = Mathf.Max(scoreboardSave.maxScore, mainScene.curScore);
            if (txt_HighestScore)
                txt_HighestScore.text = scoreboardSave.maxScore.ToString();
            scoreboardSave.curScore = 0;
			GameplayStatics.SaveGame(scoreboardSave, "ScoreboardSave.space");
		}
        else
        {
            scoreboardSave = new ScoreboardSave();
            scoreboardSave.curScore = 0;
            scoreboardSave.maxScore = mainScene.curScore;
			GameplayStatics.SaveGame(scoreboardSave, "ScoreboardSave.space");
		}
		if (txt_HighestScore)
			txt_HighestScore.text = scoreboardSave.maxScore.ToString();
		if (playerInfo != null)
        {
			playerInfo.playerHealth = GameConstants.Max_MainCharater_Health;
			GameplayStatics.SaveGame(playerInfo, "PlayerInfo.space");
		}
		GameplayStatics.SaveGame(gameProgress, "GameProgress.space");
        Time.timeScale = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgainClicked()
    {
		Debug.Log("Try again clicked !");
		SceneManager.LoadScene("MainScene");
	}

    public void MainMenuClicked()
    {
        Debug.Log("Main menu clicked !");
        SceneManager.LoadScene("MainMenu");
    }
}
