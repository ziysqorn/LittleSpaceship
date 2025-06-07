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
	[SerializeField] Button btn_DoQuiz;
	[SerializeField] Button btn_Done;
    [SerializeField] GameObject weaponPriceSection;
	[SerializeField] PrefabData prefData;
	[SerializeField] List<Sprite> weaponIcons;
    protected List<KeyValuePair<Type, GameObject>> weaponTypes;
    protected string currentWeapon = "";
	int HealthPrice = 50;
    int currentWeaponIdx = 0;
    List<KeyValuePair<string, int>> weapons;
    Dictionary<string, int> weaponLevels = new Dictionary<string, int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
		if (playerInput != null)
		{
			playerInput.inputMap.UI.Disable();
		}
		weaponLevels.Add("NormalRocket", 0);
        weaponLevels.Add("ArmorPierce", 1);
        weaponLevels.Add("LaserBeam", 2);
        weapons = new List<KeyValuePair<string, int>> {
            new KeyValuePair<string, int>("Armor Pierce", 120),
            new KeyValuePair<string, int>("Laser Beam", 200),
            new KeyValuePair<string, int>("Max", 0)
            };
        if(prefData && prefData.armorPiercePref && prefData.laserBeamPref)
        {
			weaponTypes = new List<KeyValuePair<Type, GameObject>> {
			new KeyValuePair<Type, GameObject>(typeof(NormalShooting), prefData.armorPiercePref),
			new KeyValuePair<Type, GameObject>(typeof(BeamShooting), prefData.laserBeamPref),
			new KeyValuePair<Type, GameObject>(null, null)
			};
		}
		//
		if (btn_Done != null) btn_Done.onClick.AddListener(CloseWindow);
        if (btn_Upgrade != null) btn_Upgrade.onClick.AddListener(UpgradeWeapon);
        if (btn_BuyHealth != null) btn_BuyHealth.onClick.AddListener(BuyHealth);
		if (btn_DoQuiz != null) btn_DoQuiz.onClick.AddListener(DoQuiz);
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
		PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
		if (playerInput != null)
		{
			playerInput.inputMap.UI.Enable();
		}
		Cursor.visible = false;
        Time.timeScale = 1.0f;
		PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
        ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
        SavedCurrentWeapon savedCurrentWeapon = GameplayStatics.LoadGame<SavedCurrentWeapon>("SavedCurrentWeapon.space");
		if (playerInfo == null) playerInfo = new PlayerInfo();
        if (scoreboardSave == null) scoreboardSave = new ScoreboardSave();
        if (savedCurrentWeapon == null) savedCurrentWeapon = new SavedCurrentWeapon();
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
        savedCurrentWeapon.currentWeapon = currentWeapon;
        GameplayStatics.SaveGame(savedCurrentWeapon, "SavedCurrentWeapon.space");
		UIManager uiManager = FindFirstObjectByType<UIManager>();
		uiManager?.popOutUI();
        if (mainCharacter)
        {
			AttackComponent attackComponent = mainCharacter.GetComponent<AttackComponent>();
			attackComponent?.Attack();
		}
		Destroy(gameObject);
    }

    protected void UpgradeWeapon()
    {
		MainCharacter mainCharacter = FindFirstObjectByType<MainCharacter>();
		if (mainCharacter)
		{
			string result = "";
			if (checkAbleToBuy(weapons[currentWeaponIdx].Value))
			{
				scoreConsume(weapons[currentWeaponIdx].Value);
				AttackComponent attackComponent = mainCharacter.GetComponent<AttackComponent>();
                if (attackComponent != null) {
                    RangedAttack rangedAttack = (RangedAttack)Activator.CreateInstance(weaponTypes[currentWeaponIdx].Key, mainCharacter.gameObject, weaponTypes[currentWeaponIdx].Value);
                    rangedAttack.SetCurMode(1);
                    rangedAttack.UpdateCurMode();
                    attackComponent.SetStrategy(rangedAttack);
                    currentWeapon = weapons[currentWeaponIdx].Key.Replace(" ", "");
					++currentWeaponIdx;
                    UpdateWeaponInfo();
					result = "Weapon upgraded !";
				}
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

    protected void SetupWeaponInfo()
    {
        SavedCurrentWeapon savedCurrentWeapon = GameplayStatics.LoadGame<SavedCurrentWeapon>("SavedCurrentWeapon.space");
        if(savedCurrentWeapon != null)
        {
            if (weaponLevels.ContainsKey(savedCurrentWeapon.currentWeapon))
            {
                currentWeaponIdx = weaponLevels[savedCurrentWeapon.currentWeapon];
                currentWeapon = savedCurrentWeapon.currentWeapon;
            }
        }
        else
        {
            currentWeaponIdx = 0;
			currentWeapon = "NormalRocket";
		}
        UpdateWeaponInfo();
	}

    protected void UpdateWeaponInfo()
    {
		if (btn_Upgrade) btn_Upgrade.interactable = true;
		if (weaponPriceSection)
			weaponPriceSection.SetActive(true);
		if (txt_weaponName)
			txt_weaponName.text = weapons[currentWeaponIdx].Key;
		if (img_weaponIcon)
			img_weaponIcon.sprite = weaponIcons[currentWeaponIdx];
		if (currentWeaponIdx == weapons.Count - 1)
        {
			if (btn_Upgrade) btn_Upgrade.interactable = false;
            if (weaponPriceSection)
                weaponPriceSection.SetActive(false);
		}
        else
        {
			if (txt_weaponPrice)
				txt_weaponPrice.text = weapons[currentWeaponIdx].Value.ToString();
		}
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

    protected void DoQuiz()
    {
        if(prefData && prefData.quizDialogWindowPref)
        {
            GameObject GB_quizDialog = Instantiate(prefData.quizDialogWindowPref);
            QuizWindow quizWindow = GB_quizDialog.GetComponent<QuizWindow>();
            if (quizWindow != null) {
				quizWindow.initQuizList(15);
                quizWindow.setRemainingTime(300.0f);
                quizWindow.OnQuizEnd += checkQuizResult;
			}
            UIManager uiManager = FindFirstObjectByType<UIManager>();
			uiManager?.addUI(GB_quizDialog);
        }
    }

    protected void checkQuizResult(int correctAnsCount, int totalQuizCount)
    {
        int percentage = (int)Math.Ceiling(((double)correctAnsCount / totalQuizCount) * 100.0f);
        if (percentage >= 80)
        {
			MainSceneScript mainSceneScript = FindFirstObjectByType<MainSceneScript>();
			if (mainSceneScript)
			{
                int amount = (int)Math.Ceiling((double)(300 * percentage / 100));
				mainSceneScript.UpdateScore(amount);
				txt_Score.text = mainSceneScript.curScore.ToString();
			}
		}
		if (prefData && prefData.screenMessagePref)
		{
			GameObject screenMessageUI = Instantiate(prefData.screenMessagePref);
			if (screenMessageUI != null)
			{
				ScreenMessageUI screenMessage = screenMessageUI.GetComponent<ScreenMessageUI>();
				if (screenMessage != null)
				{
					string result = string.Format("Correct answers: {0}/{1}", correctAnsCount, totalQuizCount);
					screenMessage.setScreenMessage(result);
					screenMessage.setMessageTextColor(Color.white);
					UIManager uiManager = FindFirstObjectByType<UIManager>();
					if(uiManager != null)
                    {
                        screenMessage.OnMessageEnd += uiManager.popOutUI;
                    }
				}
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
		btn_BuyHealth.onClick.RemoveListener(BuyHealth);
        btn_DoQuiz.onClick.RemoveListener(DoQuiz);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
