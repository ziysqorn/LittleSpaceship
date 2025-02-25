using Game.Interfaces;
using UnityEngine;

public class DoubleShot : IShootMode
{
	public void Shoot(in GameObject controller, in GameObject projectile)
	{
		if (controller && projectile)
		{
			PoolManager manager = PoolManager.poolManager;
			if (manager != null)
			{
				Projectile proc = projectile.GetComponent<Projectile>();
				if (proc)
				{
					if (proc.projectileName != "")
					{
						string procName = proc.projectileName;
						if (!manager.PoolExisted(procName)) manager.RegisterPool(procName, new ObjectPool(projectile));
						BaseCharacter character = controller.GetComponent<BaseCharacter>();
						if (character != null)
						{
							for (int i = character.sockets.Length - 1; i > 0; --i)
							{
								Vector3 spawnPos = controller.transform.position + character.sockets[i];
								GameObject bullet = manager.ActivateObjFromPool(procName, spawnPos, controller.transform.rotation);
								Projectile storedProc = bullet.GetComponent<Projectile>();
								if (storedProc) storedProc.owner = controller;
							}
						}
					}
				}
			}
		}
	}
}
