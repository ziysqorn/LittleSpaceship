using Game.Interfaces;
using Unity.Jobs;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance { get; private set; }
	public PrefabData data;
	private void Awake()
	{
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RewardProc()
    {
        GameObject reward = null;
        int rewardRand = Random.Range(1, 2);
        if (rewardRand == 1)
        {
            int procRand = Random.Range(0, 3);
            GameObject chosenProc = null;
			if (data)
            {
                chosenProc = data.collectibleProcPrefs[procRand];
            }
            INameablePrefab nameableEffect = chosenProc.GetComponent<INameablePrefab>();
            string chosenPrefName = "";
            if (nameableEffect != null)
            {
                chosenPrefName = nameableEffect.GetPrefabName();
				nameableEffect.SetPrefabName();
			}

			PoolManager manager = PoolManager.poolManager;
            if(manager && chosenProc) {
                if (!manager.PoolExisted(chosenPrefName)) manager.RegisterPool(chosenPrefName, new ObjectPool(chosenProc));
                reward = manager.ActivateObjFromPool(chosenPrefName, new Vector3(0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            }
        }
        return reward;
    }
}
