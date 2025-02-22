using Game.Interfaces;
using UnityEngine;

public class TripleBeam : IShootMode
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
					if (nameable != null) nameable.SetProcName();
					if (proc.projectileName != "")
					{
						string procName = proc.projectileName;
						if (!manager.PoolExisted(procName)) manager.RegisterPool(procName, new ObjectPool(projectile));
						BaseCharacter character = controller.GetComponent<BaseCharacter>();
						if (character != null)
						{
							for (int i = 0; i < character.sockets.Length; ++i)
							{
								GameObject beam = manager.ActivateObjFromPool(procName, controller.transform.position, controller.transform.rotation);
								beam.transform.SetParent(controller.transform);
								Vector3 offset = new Vector3(0.35f, -0.2f, 0.0f);
								offset.x *= -controller.transform.right.x;
								offset.y *= controller.transform.up.y;
								if (i == character.sockets.Length - 1)
								{
									offset.x *= -controller.transform.right.x;
									offset.x = 0.25f;
								}
								else if (i == 0) offset.x = -0.05f;
								beam.transform.localPosition = character.sockets[i] + offset;
								Projectile storedProc = beam.GetComponent<Projectile>();
								if (storedProc) storedProc.owner = controller;
							}
						}
					}
				}
			}
		}
	}
}
