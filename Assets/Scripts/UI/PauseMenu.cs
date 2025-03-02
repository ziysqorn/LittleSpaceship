using GlobalAccess;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeClicked()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Destroy(gameObject);
    }

	public void MainMenuClicked()
	{
		GameProgress gameProgress = new GameProgress();
		MainSceneScript mainScene = FindFirstObjectByType<MainSceneScript>();
		ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
		PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
		if (mainScene != null)
		{
			if(scoreboardSave == null) scoreboardSave = new ScoreboardSave();
			scoreboardSave.curScore = mainScene.curScore;
			gameProgress.curWave = mainScene.curWave;
			GameplayStatics.SaveGame(scoreboardSave, "ScoreboardSave.space");
			GameplayStatics.SaveGame(gameProgress, "GameProgress.space");
		}
		if(playerInfo == null) playerInfo = new PlayerInfo();
		MainCharacter mainCharacter = FindFirstObjectByType<MainCharacter>();
		if (mainCharacter != null)
		{
			playerInfo.playerHealth = mainCharacter.CurrentHealth;
			GameplayStatics.SaveGame(playerInfo, "PlayerInfo.space");
		}
		Time.timeScale = 0.0f;
		SceneManager.LoadScene("MainMenu");
	}
}
