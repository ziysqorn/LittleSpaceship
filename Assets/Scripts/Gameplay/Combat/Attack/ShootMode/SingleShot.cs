using Game.Interfaces;
using UnityEngine;

public class SingleShot : IShootMode
{
	public void Shoot(in GameObject controller, in GameObject projectile)
	{
		if (controller && projectile)
		{
			GameObject bullet = GameObject.Instantiate(projectile, controller.transform.position, controller.transform.rotation);
			Projectile proc = bullet.GetComponent<Projectile>();
			if (proc) proc.owner = controller;
		}
	}
}
