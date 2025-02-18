using Game.Interfaces;
using UnityEngine;

public class SingleShot : IShootMode
{
	public void Shoot(in GameObject controller, in GameObject projectile)
	{
		if (controller && projectile)
		{
			PoolManager manager = PoolManager.poolManager;
			if (manager != null)
			{
				if (!manager.PoolExisted("Projectile")) manager.RegisterPool("Projectile", new ObjectPool(projectile));
				GameObject bullet = manager.ActivateObjFromPool("Projectile", controller.transform.position, controller.transform.rotation);
				Projectile proc = bullet.GetComponent<Projectile>();
				if (proc) proc.owner = controller;
			}
		}
	}
}
