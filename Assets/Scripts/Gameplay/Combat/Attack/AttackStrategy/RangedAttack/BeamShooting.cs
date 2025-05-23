using Game.Interfaces;
using UnityEngine;

public class BeamShooting : RangedAttack
{

	public BeamShooting()
	{

	}

	public BeamShooting(in GameObject owner, in GameObject projectile) : base(owner, projectile)
	{
	}

	public override void Attack()
	{
		if (owner && projectile)
		{
			BaseCharacter character = owner.GetComponent<BaseCharacter>();
			if (character)
			{
				AttackComponent attackComponent = character.GetComponent<AttackComponent>();
				if (attackComponent)
				{
					attackComponent.StopAllCoroutines();
					shootMode?.Shoot(owner, projectile);
				}
			}
		}
	}

	public void SetShootMode(in IShootMode newShootMode)
	{
		if (newShootMode != null) shootMode = newShootMode;
	}

	public void SetOwner(in GameObject newOwner)
	{
		if (owner != null) owner = newOwner;
	}

	public void SetProjectile(in GameObject newProjectile)
	{
		if (newProjectile != null) projectile = newProjectile;
	}

	public override void UpdateCurMode()
	{
		switch (currentMode)
		{
			case 1:
				shootMode = new SingleBeam();
				break;
			case 2:
				shootMode = new DoubleBeam();
				break;
			case 3:
				shootMode = new TripleBeam();
				break;
		}
	}
}
