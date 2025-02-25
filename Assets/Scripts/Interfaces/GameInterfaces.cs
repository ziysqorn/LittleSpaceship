using UnityEngine;

namespace Game.Interfaces
{
	public interface IAttackStrategy
	{
		public void Attack();
	}
	public interface IShootMode
	{
		public void Shoot(in GameObject controller, in GameObject projectile);
	}
	public interface IDamageable
	{
		public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser);
	}

	public interface IFightingStyle
	{

	}
}