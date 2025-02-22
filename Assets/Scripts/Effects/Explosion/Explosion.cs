using System.Collections;
using UnityEngine;

public abstract class Explosion : MonoBehaviour
{
    public string effectName = "";
    protected Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	protected virtual void OnEnable()
	{
		animator = GetComponent<Animator>();
		if (animator)
		{
			var transInfo = animator.GetCurrentAnimatorStateInfo(0);
            StartCoroutine(CallSelfDestroy(transInfo.length));
		}
	}

    protected IEnumerator CallSelfDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        SelfDestroy();
    }

	protected void SelfDestroy()
    {
        PoolManager poolManager = PoolManager.poolManager;
        if (poolManager)
        {
            poolManager.RetrieveObjToPool(effectName, gameObject);
        }
    }
}
