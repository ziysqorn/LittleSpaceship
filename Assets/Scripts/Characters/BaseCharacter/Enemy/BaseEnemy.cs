using Game.Interfaces;
using GlobalAccess;
using System.Collections;
using UnityEditor.Analytics;
using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
	protected IFightingStyle fightingStyle;
	protected AttackComponent attackComp;
	[SerializeField] protected PrefabData prefData;
	protected override void Awake()
	{
        base.Awake();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected void OnEnable()
	{
		MaxHealth = GameConstants.Max_MainCharater_Health;
		CurrentHealth = MaxHealth;
		bIsDead = false;
		if(!attackComp) attackComp = gameObject.GetComponent<AttackComponent>();
		if (attackComp)
		{
			float randAtkSpeed = Random.Range(GameConstants.Enemy_AttackSpeed, 20.0f);
			attackComp.SetAttackSpeed(randAtkSpeed);
			attackComp.SetDamage(GameConstants.Enemy_Damage);
			if (prefData)
			{
				NormalShooting attackStrategy = new NormalShooting(gameObject, prefData.normalRocketPref);
				attackStrategy.SetShootMode(new SingleShot());
				attackComp.SetStrategy(attackStrategy);
				StartCoroutine(ReleaseAttack(randAtkSpeed));
			}
		}
	}

	protected override void Death()
	{
		base.Death();
		RewardManager rewardManager = RewardManager.instance;
		if (rewardManager)
		{
			GameObject reward = rewardManager.RewardProc();
			if(reward) reward.transform.position = gameObject.transform.position;
		}
		PoolManager manager = PoolManager.poolManager;
		if (manager)
		{
			gameObject.transform.parent = null;
			manager.RetrieveObjToPool(characterName, gameObject);
		}
	}

	public T GetFightingStyle<T>() where T : class, IFightingStyle
	{
		return fightingStyle as T;
	}

	protected void SetFightingStyle(in IFightingStyle style)
	{
		fightingStyle = style;
	}

	IEnumerator ReleaseAttack(float delay)
	{
		yield return new WaitForSeconds(delay);
		attackComp.Attack();
	}
}
