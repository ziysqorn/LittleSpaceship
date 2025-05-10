using Game.Interfaces;
using System.Collections;
using UnityEngine;

public class NormalShooting : RangedAttack
{
	public NormalShooting(in GameObject owner, in GameObject projectile)
	{
		this.owner = owner;
		this.projectile = projectile;
	}

	public override void Attack()
	{
		if (owner && projectile)
		{
			BaseCharacter character = owner.GetComponent<BaseCharacter>();
			if (character)
			{
				AttackComponent attackComponent = character.GetComponent<AttackComponent>();
				attackComponent.StopAllCoroutines();
				if (attackComponent) {
					attackComponent.StartCoroutine(AttackLoop(attackComponent.AttackSpeed));
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

	public override void UpdateCurMode()
	{
		switch (currentMode)
		{
			case 1:
				shootMode = new SingleShot();
				break;
			case 2:
				shootMode = new DoubleShot();
				break;
			case 3:
				shootMode = new TripleShot();
				break;
		}
	}
}
