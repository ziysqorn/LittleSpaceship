using Game.Interfaces;
using UnityEngine;

public class GameplayStatics
{
	public static void ApplyDamage(in GameObject damagedObj, int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		if (damagedObj)
		{
			IDamageable damageable = damagedObj.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damageAmount, instigator, damageCauser);
			}
		}
	}
}
