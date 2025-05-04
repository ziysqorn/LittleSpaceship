using TreeEditor;
using UnityEngine;

public class CollectiblesInvinciShield : Collectibles
{
	[SerializeField] protected PrefabData prefData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected override void ApplyEffect(in GameObject appliedObj)
	{
		PoolManager manager = PoolManager.poolManager;
		if (manager && prefData && prefData.shieldPref)
		{
			Shield shield = prefData.shieldPref.GetComponent<Shield>();
			if (shield)
			{
				if (!manager.PoolExisted(shield.shieldName)) manager.RegisterPool(shield.shieldName, new ObjectPool(prefData.shieldPref));
				GameObject newShield = manager.ActivateObjFromPool(shield.shieldName, gameObject.transform.position, gameObject.transform.rotation);
				if (newShield) newShield.transform.parent = transform;
			}
		}
	}
}
