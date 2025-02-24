using Game.Interfaces;
using UnityEngine;

public abstract class RangedAttack : IAttackStrategy
{
	protected IShootMode shootMode;
	protected GameObject owner;
	protected GameObject projectile;
	protected int currentMode = 1;
	public abstract void Attack();
	public abstract void UpdateCurMode();
	public int IncreaseCurMode(in string collectedProcName)
	{
		if (projectile)
		{
			Projectile proc = projectile.GetComponent<Projectile>();
			if (proc && proc.projectileName == collectedProcName)
			{
				++currentMode;
				if (currentMode > 3) currentMode = 3;
			}
			else currentMode = 1;
		}
		return currentMode;
	}
	public void SetCurMode(int mode)
	{
		this.currentMode = mode;
	}
	public void SetShootMode(IShootMode newShootMode)
	{
		this.shootMode = newShootMode;
	}
}
