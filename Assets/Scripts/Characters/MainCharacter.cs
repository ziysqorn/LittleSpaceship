using UnityEngine;
using GlobalAccess;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Interfaces;

public class MainCharacter : BaseCharacter, IDamageable
{
    protected float Speed;
    protected AttackComponent attackComp;
	[SerializeField] protected PrefabData prefData;

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
        attackComp?.Attack();
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
		if (attackComp)
		{
			attackComp.SetAttackSpeed(GameConstants.MainCharacter_AttackSpeed);
			attackComp.SetDamage(GameConstants.MainCharacter_Damage);
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
			PoolManager manager = PoolManager.poolManager;
			if(manager && prefData.shieldPref)
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
					Instantiate(prefData.quizDialogWindowPref);
				}
				//--CurrentHealth;
				//if(CurrentHealth <= 0)
				//{
				//	if (!bIsDead)
				//	{
				//		Death();
				//		bIsDead = true;
				//		Instantiate(prefData.tryAgainMenuPref);
				//		Destroy(gameObject);
				//		return;
				//	}
				//}
				//HUD playerHUD = FindFirstObjectByType<HUD>();
				//if (playerHUD)
				//{
				//	playerHUD.DecreaseHeart();
				//}
			}
		}
	}
}
