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
	protected virtual void Start()
    {
	}

    // Update is called once per frame
    protected virtual void Update()
    {
		transform.position = Vector3.Lerp(transform.position, new Vector3(0.0f, -4.0f, 0.0f), 0.5f * Time.deltaTime);
	}

    protected abstract void SpawnEnemyWave();

    protected void DecreaseEnemyNum()
    {
        --enemyNum;
		//Debug.Log($"Enemy died. enemyNum remaining: {enemyNum}");
		if (enemyNum <= 0)
        {
            if(prefData && prefData.upgradeWindowPref)
            {
                Instantiate(prefData.upgradeWindowPref);
            }
        }
    }
}
