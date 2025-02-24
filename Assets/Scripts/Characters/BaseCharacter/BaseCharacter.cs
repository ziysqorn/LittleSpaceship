using UnityEngine;
using GlobalAccess;
using System.Collections.Generic;
using System;
using Game.Interfaces;

public abstract class BaseCharacter : MonoBehaviour
{
    //Properties
    [SerializeField] protected int MaxHealth;
    protected int CurrentHealth;

	//Booleans
	protected bool bCanBeDamaged;


    //Components
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rBody;
    [SerializeField] public Vector3[] sockets = new Vector3[3];
    [SerializeField] public GameObject destroyedExplosion;

	protected virtual void Awake()
	{
        //Component init
        boxCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();

        //Properties init
		bCanBeDamaged = true;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void Death()
    {
		PoolManager manager = PoolManager.poolManager;
		if (manager)
		{
			if (destroyedExplosion)
			{
				Explosion attachedExplosion = destroyedExplosion.GetComponent<Explosion>();
				if (attachedExplosion)
				{
					INameablePrefab nameableEffect = attachedExplosion as INameablePrefab;
					if (nameableEffect != null) nameableEffect.SetPrefabName();
				}
				if (!manager.PoolExisted(attachedExplosion.effectName)) manager.RegisterPool(attachedExplosion.effectName, new ObjectPool(destroyedExplosion));
				manager.ActivateObjFromPool(attachedExplosion.effectName, gameObject.transform.position, gameObject.transform.rotation);
			}
		}
	}
}
