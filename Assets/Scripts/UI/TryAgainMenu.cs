using GlobalAccess;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgainMenu : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txt_HighestScore;
    [SerializeField] protected Button btn_TryAgain;
	[SerializeField] protected Button btn_MainMenu;
	[SerializeField] protected bool isGameOverMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
		if (playerInput != null)
		{
			playerInput.inputMap.UI.Disable();
		}
		SoundManager soundManager = SoundManager.instance;
        if (soundManager)
        {
            if (isGameOverMenu)
            {
				soundManager.PlaySFX(soundManager.SFX_gameOver);
			}
			else
			{
                soundManager.PlaySFX(soundManager.SFX_congrats);
			}
		}
        Cursor.visible = true;
		GameProgress gameProgress = new GameProgress();
		gameProgress.curWave = 0;
		MainSceneScript mainScene = FindFirstObjectByType<MainSceneScript>();
		ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
        PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
		if (mainScene != null && scoreboardSave != null)
		{
            scoreboardSave.maxScore = Mathf.Max(scoreboardSave.maxScore, mainScene.curScore);
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
        if (btn_MainMenu) btn_MainMenu.onClick.AddListener(MainMenuClicked);
        if (btn_TryAgain) btn_TryAgain.onClick.AddListener(TryAgainClicked);
        Time.timeScale = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnDestroy()
	{
        if (btn_MainMenu) btn_MainMenu.onClick.RemoveListener(MainMenuClicked);
		if (btn_TryAgain) btn_TryAgain.onClick.RemoveListener(TryAgainClicked);
	}

	public void TryAgainClicked()
    {
		PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
		if (playerInput != null)
		{
			playerInput.inputMap.UI.Enable();
		}
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

    public void MainMenuClicked()
    {
		PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
		if (playerInput != null)
		{
			playerInput.inputMap.UI.Enable();
		}
		GameplayStatics.DeleteSaveGame("GameProgress.space");
        SceneManager.LoadScene("MainMenu");
    }
}
