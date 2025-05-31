using Game.Interfaces;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] public string shieldName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	protected void OnEnable()
	{
        DelayDisableShield(3.0f);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public void DelayDisableShield(float inDelay)
    {
        CancelInvoke("DisableShield");
        Invoke("DisableShield", inDelay);
    }

    protected void DisableShield()
    {
        transform.parent = null;
        PoolManager manager = PoolManager.poolManager;
        if (manager)
        {
            manager.RetrieveObjToPool(shieldName, gameObject);
        }
    }
}
