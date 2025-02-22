using Game.Interfaces;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NormalRocket : Rocket, INameableEffect
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

	public void SetProcName()
	{
		projectileName = "NormalRocket";
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		BaseCharacter target = collision.gameObject.GetComponent<BaseCharacter>();
		BaseCharacter instigator = owner.GetComponent<BaseCharacter>();
		if (target && target != instigator && target.gameObject.tag != instigator.gameObject.tag)
		{
			PoolManager poolManager = PoolManager.poolManager;
			if (poolManager)
			{
				if (explosion)
				{
					Explosion attachedExplosion = explosion.GetComponent<Explosion>();
					if (attachedExplosion)
					{
						INameableEffect nameableEffect = attachedExplosion as INameableEffect;
						if (nameableEffect != null) nameableEffect.SetProcName();
					}
					if (!poolManager.PoolExisted(attachedExplosion.effectName)) poolManager.RegisterPool(attachedExplosion.effectName, new ObjectPool(explosion));
					poolManager.ActivateObjFromPool(attachedExplosion.effectName, gameObject.transform.position, gameObject.transform.rotation);
				}
				gameObject.SetActive(false);
			}
		}
	}
}
