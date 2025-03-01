using Game.Interfaces;
using UnityEngine;

public class Nairan : BaseEnemy, IDamageable
{
	protected override void Awake()
	{
		base.Awake();
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

	protected override void Death()
	{
		base.Death();
	}

	public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		CurrentHealth -= damageAmount;
		if (CurrentHealth <= 0)
		{
			if (!bIsDead)
			{
				Death();
				bIsDead = true;
			}
		}
	}
}
