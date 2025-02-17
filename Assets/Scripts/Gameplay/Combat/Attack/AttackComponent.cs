using Game.Interfaces;
using System.Collections;
using UnityEngine;

public class AttackComponent
{
	protected GameObject owner;
	protected IAttackStrategy strategy;

	public AttackComponent(in GameObject owner, in IAttackStrategy strategy)
	{
		this.owner = owner;
		this.strategy = strategy;
	}

	public void Attack()
	{
		strategy?.Attack();
	}

	public IEnumerator AttackAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		strategy?.Attack();
	}

	public IEnumerator AttackLoop(float delay)
	{
		while (true)
		{
			strategy?.Attack();
			yield return new WaitForSeconds(delay);
		}
	}
}
