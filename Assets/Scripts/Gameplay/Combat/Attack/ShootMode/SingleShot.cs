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
				BaseCharacter character = controller.GetComponent<BaseCharacter>();
				if (character != null) {
					Vector3 spawnPos = controller.transform.position + character.sockets[0];
					GameObject bullet = manager.ActivateObjFromPool("Projectile", spawnPos, controller.transform.rotation);
					Projectile proc = bullet.GetComponent<Projectile>();
					if (proc) proc.owner = controller;
				}
			}
		}
	}
}
