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
	public interface INameableEffect
	{
		public void SetProcName();
	}
}