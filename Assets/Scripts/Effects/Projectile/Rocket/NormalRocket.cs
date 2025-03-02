using UnityEngine;

public class NormalRocket : Rocket
{
	protected override void Awake()
	{
		base.Awake();
        bulletDamage = 0;
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject target = collision.gameObject;
		BaseCharacter instigator = owner.GetComponent<BaseCharacter>();
		if (target && target != instigator && target.gameObject.tag != instigator.gameObject.tag)
		{
			Shield shield = target.GetComponent<Shield>();
			if(shield)
			{
				if(target.transform.parent.gameObject != owner)
				{
					PoolManager poolManager = PoolManager.poolManager;
					if (poolManager)
					{
						if (explosion)
						{
							Explosion attachedExplosion = explosion.GetComponent<Explosion>();
							if (attachedExplosion && attachedExplosion.effectName != "")
							{
								if (!poolManager.PoolExisted(attachedExplosion.effectName)) poolManager.RegisterPool(attachedExplosion.effectName, new ObjectPool(explosion));
								poolManager.ActivateObjFromPool(attachedExplosion.effectName, gameObject.transform.position, gameObject.transform.rotation);
							}
						}
						AttackComponent attackComp = instigator.GetComponent<AttackComponent>();
						if (attackComp) GameplayStatics.ApplyDamage(target, bulletDamage + attackComp.Damage, owner, gameObject);
						gameObject.SetActive(false);
					}
				}
			}
			else
			{
				PoolManager poolManager = PoolManager.poolManager;
				if (poolManager)
				{
					if (explosion)
					{
						Explosion attachedExplosion = explosion.GetComponent<Explosion>();
						if (attachedExplosion && attachedExplosion.effectName != "")
						{
							if (!poolManager.PoolExisted(attachedExplosion.effectName)) poolManager.RegisterPool(attachedExplosion.effectName, new ObjectPool(explosion));
							poolManager.ActivateObjFromPool(attachedExplosion.effectName, gameObject.transform.position, gameObject.transform.rotation);
						}
					}
					AttackComponent attackComp = instigator.GetComponent<AttackComponent>();
					if (attackComp) GameplayStatics.ApplyDamage(target, bulletDamage + attackComp.Damage, owner, gameObject);
					gameObject.SetActive(false);
				}
			}
		}
	}
}
