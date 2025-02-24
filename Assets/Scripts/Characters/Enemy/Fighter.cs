using Game.Interfaces;
using UnityEngine;

public class Fighter : BaseEnemy, IDamageable
{
	protected override void Awake()
	{
		base.Awake();
		//MaxHealth = GameConstants.Max_Enemy_Health;
		CurrentHealth = MaxHealth;
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		CurrentHealth -= damageAmount;
		if (CurrentHealth <= 0)
		{
			Death();
			gameObject.SetActive(false);
		}
	}

	protected override void Death()
	{
		base.Death();
	}
}
