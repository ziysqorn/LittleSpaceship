using Game.Interfaces;
using System.Collections;
using UnityEngine;

public class NormalShooting : IAttackStrategy
{
	private IShootMode shootMode;
	protected GameObject owner;
	protected GameObject projectile;
	protected int currentMode = 0;

	public NormalShooting(in GameObject owner, in IShootMode shootMode, in GameObject projectile)
	{
		this.owner = owner;
		this.shootMode = shootMode;
		this.projectile = projectile;
	}

	public void Attack()
	{
		if (owner && projectile)
		{
			BaseCharacter character = owner.GetComponent<BaseCharacter>();
			if (character)
			{
				AttackComponent attackComponent = character.GetComponent<AttackComponent>();
				if (attackComponent) {
					character.StartCoroutine(AttackLoop(attackComponent.GetAttackSpeed()));
				}
			}
		}
	}

	public IEnumerator AttackLoop(float delay)
	{
		while (true)
		{
			shootMode?.Shoot(owner, projectile);
			yield return new WaitForSeconds(delay);
		}
	}

	public void SetShootMode(in IShootMode newShootMode)
	{
		if(newShootMode != null) shootMode = newShootMode;
	}

	public void SetOwner(in GameObject newOwner)
	{
		if (owner != null) owner = newOwner;
	}

	public void SetProjectile(in GameObject newProjectile)
	{
		if(newProjectile != null) projectile = newProjectile;
	}
}
