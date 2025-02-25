using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWave : MonoBehaviour
{
    [SerializeField] protected PrefabData prefData;
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
}
