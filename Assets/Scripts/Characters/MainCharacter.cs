using UnityEngine;
using GlobalAccess;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Interfaces;
using System;
using UnityEngine.TextCore.Text;

public class MainCharacter : BaseCharacter, IDamageable
{
    protected float Speed;
    protected AttackComponent attackComp;
	[SerializeField] public PrefabData prefData;

	protected PlayerInput playerInput;

	public bool bCanMove = true;
	protected override void Awake()
    {
		base.Awake();
	}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		playerInput = GetComponent<PlayerInput>();
        Cursor.visible = false;
    }

	protected void OnEnable()
	{
		MaxHealth = GameConstants.Max_MainCharater_Health;
		PlayerInfo playerInfo = GameplayStatics.LoadGame<PlayerInfo>("PlayerInfo.space");
		if (playerInfo != null)
		{
			CurrentHealth = playerInfo.playerHealth;
		}
		else CurrentHealth = MaxHealth;
		HUD playerHUD = FindFirstObjectByType<HUD>();
		if (playerHUD)
		{
			playerHUD.LoadHeart(CurrentHealth);
		}
		Speed = GameConstants.MainCharacter_Speed;
		bIsDead = false; 

		attackComp = gameObject.GetComponent<AttackComponent>();
		SavedCurrentWeapon savedCurrentWeapon = GameplayStatics.LoadGame<SavedCurrentWeapon>("SavedCurrentWeapon.space");
		if (savedCurrentWeapon == null)
		{
			savedCurrentWeapon = new SavedCurrentWeapon();
			savedCurrentWeapon.currentWeapon = "NormalRocket";
			GameplayStatics.SaveGame(savedCurrentWeapon, "SavedCurrentWeapon.space");
		}
		if (attackComp)
		{
			DatabaseManager dbManager = FindAnyObjectByType<DatabaseManager>();
			if (dbManager)
			{
				CombatDatabase combatDb = dbManager.combatDb;
				if (combatDb != null && combatDb.getAtkStrategy(savedCurrentWeapon.currentWeapon).HasValue) {
					KeyValuePair<Type, GameObject> atkStrategy = combatDb.getAtkStrategy(savedCurrentWeapon.currentWeapon).Value;
					RangedAttack rangedAttack = (RangedAttack)Activator.CreateInstance(atkStrategy.Key, gameObject, atkStrategy.Value);
					rangedAttack.SetCurMode(1);
					rangedAttack.UpdateCurMode();
					attackComp.SetStrategy(rangedAttack);
					attackComp.SetAttackSpeed(GameConstants.MainCharacter_AttackSpeed);
					attackComp.SetDamage(GameConstants.MainCharacter_Damage);
					attackComp.Attack();
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        Move();
	}

    protected void Move(){
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, mousePos, Speed * Time.deltaTime);
    }

	public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		if (prefData)
		{
			//Shield shield = prefData.shieldPref.GetComponent<Shield>();
			//if (shield)
			//{
			//	if (!manager.PoolExisted(shield.shieldName)) manager.RegisterPool(shield.shieldName, new ObjectPool(prefData.shieldPref));
			//	GameObject newShield = manager.ActivateObjFromPool(shield.shieldName, gameObject.transform.position, gameObject.transform.rotation);
			//	if(newShield) newShield.transform.parent = transform;
			//}
			if (prefData.quizDialogWindowPref)
			{
				GameObject quizDialogWindow = Instantiate(prefData.quizDialogWindowPref);
				QuizWindow quizWindow = quizDialogWindow.GetComponent<QuizWindow>();
				if (quizWindow != null)
				{
					quizWindow.initQuizList(1);
					quizWindow.setRemainingTime(10.0f);
					quizWindow.setOwner(this);
					quizWindow.OnQuizEnd += Hurt;
				}
				UIManager uiManager = FindFirstObjectByType<UIManager>();
				uiManager?.addUI(quizDialogWindow);
			}
		}
	}

	public void checkCurrentHealth()
	{
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		UIManager uiManager = FindFirstObjectByType<UIManager>();
		uiManager?.popOutUI();
		if (CurrentHealth <= 0)
		{
			if (!bIsDead)
			{
				Death();
				bIsDead = true;
			}
		}
		else bIsDead = false;
		if (!bIsDead)
		{
			if (prefData && prefData.shieldPref)
			{
				PoolManager manager = PoolManager.poolManager;
				Shield shield = prefData.shieldPref.GetComponent<Shield>();
				if (shield)
				{
					if (!manager.PoolExisted(shield.shieldName)) manager.RegisterPool(shield.shieldName, new ObjectPool(prefData.shieldPref));
					GameObject newShield = manager.ActivateObjFromPool(shield.shieldName, gameObject.transform.position, gameObject.transform.rotation);
					if (newShield) newShield.transform.parent = gameObject.transform;
				}
			}
		}
		else
		{
			Instantiate(prefData.tryAgainMenuPref);
			Destroy(gameObject);
		}
	}

	public void Hurt(int correctAnsCount, int totalAnsCount)
	{
		if (correctAnsCount != totalAnsCount)
		{
			--CurrentHealth;
			HUD playerHUD = FindFirstObjectByType<HUD>();
			if (playerHUD)
			{
				playerHUD.UpdateHeart(false);
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
					string result = string.Format("Correct answers: {0}/{1}", correctAnsCount, totalAnsCount);
					screenMessage.setScreenMessage(result);
					screenMessage.setMessageTextColor(Color.white);
					screenMessage.OnMessageEnd += checkCurrentHealth;
				}
			}
		}
	}

	public bool setHealth(int inHealth)
	{
		int updatedHealth = CurrentHealth + inHealth;

		if (updatedHealth > 0 && updatedHealth <= MaxHealth)
		{
			HUD playerHUD = FindFirstObjectByType<HUD>();
			if (updatedHealth > CurrentHealth)
			{
				if (playerHUD) playerHUD.UpdateHeart(true);
			}
			else
			{
				if (playerHUD) playerHUD.UpdateHeart(false);
			}
			CurrentHealth = updatedHealth;
			return true;
		}
		return false;
	}
}
