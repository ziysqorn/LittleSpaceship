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
				Projectile proc = projectile.GetComponent<Projectile>();
				if (proc)
				{
					INameableEffect nameable = proc as INameableEffect;
					if(nameable != null) nameable.SetProcName();
					if(proc.projectileName != "")
					{
						string procName = proc.projectileName;
						if (!manager.PoolExisted(procName)) manager.RegisterPool(procName, new ObjectPool(projectile));
						BaseCharacter character = controller.GetComponent<BaseCharacter>();
						if (character != null)
						{
							Vector3 spawnPos = controller.transform.position + character.sockets[0];
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
