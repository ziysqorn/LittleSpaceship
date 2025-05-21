using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] Image img_weaponIcon;
    [SerializeField] TextMeshProUGUI txt_Score;
    [SerializeField] TextMeshProUGUI txt_weaponName;
    [SerializeField] TextMeshProUGUI txt_weaponPrice;
    [SerializeField] TextMeshProUGUI txt_healthPrice;
    [SerializeField] Button btn_Upgrade;
    [SerializeField] Button btn_BuyHealth;
    [SerializeField] Button btn_Done;
    [SerializeField] GameObject weaponPriceSection;
	[SerializeField] PrefabData prefData;
	[SerializeField] List<Sprite> weaponIcons;
	int HealthPrice = 50;
    int currentWeaponIdx = 0;
    List<KeyValuePair<string, int>> weapons;
    Dictionary<string, int> weaponLevels = new Dictionary<string, int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		weaponLevels.Add("NormalRocket", 0);
        weaponLevels.Add("ArmorPierce", 1);
        weaponLevels.Add("LaserBeam", 2);
        weapons = new List<KeyValuePair<string, int>> {
            new KeyValuePair<string, int>("Armor Pierce", 800),
            new KeyValuePair<string, int>("Laser Beam", 1600),
            new KeyValuePair<string, int>("Max", 0)
            };
		//
		if (btn_Done != null) btn_Done.onClick.AddListener(CloseWindow);
        if (btn_Upgrade != null) btn_Upgrade.onClick.AddListener(UpgradeWeapon);
        if (btn_BuyHealth != null) btn_BuyHealth.onClick.AddListener(BuyHealth);
        //
        if (txt_Score)
        {
            MainSceneScript mainSceneScript = FindFirstObjectByType<MainSceneScript>();
            if (mainSceneScript) txt_Score.text = mainSceneScript.curScore.ToString();
        }
        if (txt_healthPrice)
        {
            txt_healthPrice.text = HealthPrice.ToString();
        }
        //
        SetupWeaponInfo();

        //
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        //
		UIManager uiManager = FindFirstObjectByType<UIManager>();
		uiManager?.addUI(gameObject);
	}

    protected void CloseWindow()
    {
        Cursor.visible = false;
        Time.timeScale = 1.0f;
		MainSceneScript mainScene = FindFirstObjectByType<MainSceneScript>();
		mainScene.IncreaseCurWave();
		PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
        ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
		if (playerInfo == null) playerInfo = new PlayerInfo();
        if (scoreboardSave == null) scoreboardSave = new ScoreboardSave();
		MainCharacter mainCharacter = FindFirstObjectByType<MainCharacter>();
		if (mainCharacter)
		{
			playerInfo.playerHealth = mainCharacter.CurrentHealth;
            GameplayStatics.SaveGame(playerInfo, "PlayerInfo.space");
		}
        MainSceneScript mainSceneScript = FindFirstObjectByType<MainSceneScript>();
        if (mainSceneScript)
        {
            scoreboardSave.curScore = mainSceneScript.curScore;
			GameplayStatics.SaveGame(scoreboardSave, "ScoreboardSave.space");
		}
		UIManager uiManager = FindFirstObjectByType<UIManager>();
		uiManager?.popOutUI();
		Destroy(gameObject);
    }

    protected void UpgradeWeapon()
    {
        
    }

    protected void SetupWeaponInfo()
    {
        SavedCurrentWeapon savedCurrentWeapon = GameplayStatics.LoadGame<SavedCurrentWeapon>("SavedCurrentWeapon.space");
        if(savedCurrentWeapon != null)
        {
            if (weaponLevels.ContainsKey(savedCurrentWeapon.currentWeapon))
            {
                currentWeaponIdx = weaponLevels[savedCurrentWeapon.currentWeapon];
            }
        }
        else
        {
            currentWeaponIdx = 0;
        }
        if (txt_weaponName)
            txt_weaponName.text = weapons[currentWeaponIdx].Key;
        if (txt_weaponPrice)
			txt_weaponPrice.text = weapons[currentWeaponIdx].Value.ToString();
        if (img_weaponIcon)
            img_weaponIcon.sprite = weaponIcons[currentWeaponIdx];
	}

    protected void BuyHealth()
    {
        MainCharacter mainCharacter = FindFirstObjectByType<MainCharacter>();
        if (mainCharacter)
        {
            string result = "";
            if (checkAbleToBuy(HealthPrice))
            {
                if (mainCharacter.setHealth(1))
                {
                    scoreConsume(HealthPrice);
                    PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
                    if (playerInfo == null) playerInfo = new PlayerInfo();
                    playerInfo.playerHealth = mainCharacter.CurrentHealth;
                    GameplayStatics.SaveGame(playerInfo, "PlayerInfo.space");
                    result = "Health bought !";
                }
                else result = "Health full !";
            }
            else result = "Not enough score to buy !";
			if (prefData && prefData.dialogBoxPref)
			{
				GameObject GB_dialogBox = Instantiate(prefData.dialogBoxPref);
				DialogBox dialogBox = GB_dialogBox.GetComponent<DialogBox>();
                dialogBox?.setMessageText(result);
                UIManager uiManager = FindFirstObjectByType<UIManager>();
                uiManager?.addUI(GB_dialogBox);
			}

		}
        
    }

    protected bool checkAbleToBuy(int price)
    {
        MainSceneScript mainSceneScript = FindFirstObjectByType<MainSceneScript>();
        int score = 0;
        if (mainSceneScript)
        {
			score = mainSceneScript.curScore;
            if(score >= price) return true;
		}
		return false;
    }

    protected void scoreConsume(int amount)
    {
		MainSceneScript mainSceneScript = FindFirstObjectByType<MainSceneScript>();
		if (mainSceneScript)
		{
			mainSceneScript.UpdateScore(-amount);
			//ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
			//if (scoreboardSave == null) scoreboardSave = new ScoreboardSave();
			//scoreboardSave.curScore = mainSceneScript.curScore;
			//GameplayStatics.SaveGame(scoreboardSave, "ScoreboardSave.space");
			txt_Score.text = mainSceneScript.curScore.ToString();
		}
	}

	protected void OnDestroy()
	{
		btn_Done.onClick.RemoveListener(CloseWindow);
		btn_Upgrade.onClick.RemoveListener(UpgradeWeapon);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
