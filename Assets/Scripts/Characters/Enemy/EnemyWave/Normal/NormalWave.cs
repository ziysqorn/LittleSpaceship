using UnityEngine;

public abstract class NormalWave : EnemyWave
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
		base.Start();
    }

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	protected override void SpawnEnemyWave()
    {
		if (prefData && gameObject && usedPrefList != null)
		{
			//enemyNum = gameObject.transform.childCount;
			enemyNum = 1;
			for (int i = 0; i < enemyNum; ++i)
			{
				GameObject child = gameObject.transform.GetChild(i).gameObject;
				PoolManager manager = PoolManager.poolManager;
				if (child && manager && usedPrefList.Count > 0)
				{
					int randEnemy = Random.Range(0, usedPrefList.Count - 1);
					BaseEnemy enemy = usedPrefList[randEnemy].gameObject.GetComponent<BaseEnemy>();
					if (enemy)
					{
						if (!manager.PoolExisted(enemy.characterName)) manager.RegisterPool(enemy.characterName, new ObjectPool(usedPrefList[randEnemy]));
						GameObject ship = manager.ActivateObjFromPool(enemy.characterName, child.transform.position, child.transform.rotation);
						BaseEnemy enemyShip = ship.GetComponent<BaseEnemy>();
						if (enemyShip)
						{
							enemyShip.OnDead -= DecreaseEnemyNum;
							enemyShip.OnDead += DecreaseEnemyNum;
						}
						ship.transform.parent = child.transform;
						//ship.transform.localRotation = new Quaternion(0.0f, 0.0f, 180.0f, 0.0f);
					}
				}
			}
		}
	}
}
