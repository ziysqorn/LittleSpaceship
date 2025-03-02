using UnityEngine;

public class MainSceneScript : MonoBehaviour
{
    protected int maxWave = 3;
    public int curWave { get; private set; } = 0;
    public int curScore {get; private set;}
    [SerializeField] protected PrefabData prefData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
        curScore = 0;
        GameProgress gameProgress = GameplayStatics.LoadGame<GameProgress>("GameProgress.space");
        if (gameProgress != null)
        {
            curWave = gameProgress.curWave;
        }
        else curWave = 0;
        RewardManager rewardManager = RewardManager.instance;
        if (rewardManager) rewardManager.RewardProc();
		SpawnWave();
        LoadCurScore();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void LoadCurScore()
    {
        ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
        if (scoreboardSave != null)
        {
            curScore = scoreboardSave.curScore;
        }
        else curScore = 0;
    }

    public void UpdateScore(int score)
    {
        curScore += score;
        HUD playerHUD = FindFirstObjectByType<HUD>();
        if (playerHUD != null) {
            playerHUD.UpdateScore(curScore);
        }
    }

    public void IncreaseCurWave()
    {
        ++curWave;
        if (curWave >= maxWave) AllWaveCleared();
        else SpawnWave();
    }

    public void SpawnWave()
    {
        if (prefData)
        {
            Instantiate(prefData.enemyWavesPrefs[curWave]);
            GameProgress gameProgress = new GameProgress();
            gameProgress.curWave = curWave;
            GameplayStatics.SaveGame(gameProgress, "GameProgress.space");
		}
    }

    public void AllWaveCleared()
    {
        if (prefData)
        {
            Instantiate(prefData.congratsMenuPref);
            Time.timeScale = 0.0f;
        }
    }

    public void GameOver()
    {
        if (prefData)
        {
            Instantiate(prefData.tryAgainMenuPref);
        }
    }
}
