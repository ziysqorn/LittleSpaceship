using Game.Interfaces;
using UnityEngine;

public class NormalShooting : IAttackStrategy
{
	private IShootMode shootMode;
	protected GameObject owner;
	protected GameObject projectile;
	public NormalShooting(in GameObject owner, in IShootMode shootMode, in GameObject projectile)
	{
		this.owner = owner;
		this.shootMode = shootMode;
		this.projectile = projectile;
	}

	public void Attack()
	{
		if(owner && projectile) shootMode?.Shoot(owner, projectile);
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
