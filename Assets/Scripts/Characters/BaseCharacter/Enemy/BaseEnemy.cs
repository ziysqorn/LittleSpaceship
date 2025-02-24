using GlobalAccess;
using UnityEditor.Analytics;
using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
	protected override void Awake()
	{
        base.Awake();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected override void Death()
	{
		base.Death();
		RewardManager rewardManager = RewardManager.instance;
		if (rewardManager)
		{
			GameObject reward = rewardManager.RewardProc();
			if(reward) reward.transform.position = gameObject.transform.position;
		}
	}
}
