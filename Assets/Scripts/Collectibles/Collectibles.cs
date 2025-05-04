using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    public string collectibleName = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision != null && collision.gameObject && collision.gameObject.tag == "Player") {
            ApplyEffect(collision.gameObject);
			PoolManager manager = PoolManager.poolManager;
            if (manager) manager.RetrieveObjToPool(collectibleName, gameObject);
		}
	}

    protected abstract void ApplyEffect(in GameObject appliedObj);

}
