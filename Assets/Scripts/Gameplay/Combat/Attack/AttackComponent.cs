using Game.Interfaces;
using System.Collections;
using UnityEngine;

public class AttackComponent : MonoBehaviour 
{
	public IAttackStrategy strategy { get; private set; }
	protected float AttackSpeed;

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

	public float GetAttackSpeed()
	{
		return this.AttackSpeed;
	}
}
