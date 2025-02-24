using Game.Interfaces;
using System.Collections;
using UnityEngine;

public class AttackComponent : MonoBehaviour 
{
	public IAttackStrategy strategy { get; private set; }
	public float AttackSpeed { get; private set; }
	public int Damage { get; private set; }

	public void Attack()
	{
		strategy?.Attack();
	}

	public void SetStrategy(in IAttackStrategy strategy)
	{
		this.strategy = strategy;
	}

	public void SetAttackSpeed(in float speed)
	{
		this.AttackSpeed = speed;
	}

	public void SetDamage(in int damage)
	{
		this.Damage = damage;
	}
}
