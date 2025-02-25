using UnityEngine;

public abstract class NormalWave : EnemyWave
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SpawnEnemyWave()
    {
		if (prefData && gameObject)
		{
			for (int i = 0; i < gameObject.transform.childCount; ++i)
			{
				GameObject child = gameObject.transform.GetChild(i).gameObject;
				PoolManager manager = PoolManager.poolManager;
				if (child && manager && prefData.klaedPrefs.Count > 0)
				{
					BaseEnemy enemy = prefData.klaedPrefs[0].gameObject.GetComponent<BaseEnemy>();
					if (!manager.PoolExisted(enemy.characterName)) manager.RegisterPool(enemy.characterName, new ObjectPool(prefData.klaedPrefs[0]));
					GameObject ship = manager.ActivateObjFromPool(enemy.characterName, child.transform.position, child.transform.rotation);
					ship.transform.parent = child.transform;
				}
			}
		}
	}
}
