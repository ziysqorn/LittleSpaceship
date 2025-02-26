using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWave : MonoBehaviour
{
    [SerializeField] protected PrefabData prefData;
    protected List<GameObject> usedPrefList;
    protected int enemyNum;
	protected void Awake()
	{
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void SpawnEnemyWave();

    protected void DecreaseEnemyNum()
    {
        --enemyNum;
        if (enemyNum <= 0) Debug.Log("Wave cleared !");
    }
}
