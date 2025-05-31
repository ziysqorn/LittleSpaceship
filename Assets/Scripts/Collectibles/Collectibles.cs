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
			SoundManager soundManager = SoundManager.instance;
			if (soundManager)
			{
				soundManager.PlaySFX(soundManager.SFX_rewarded);
			}
			PoolManager manager = PoolManager.poolManager;
            if (manager) manager.RetrieveObjToPool(collectibleName, gameObject);
		}
	}

    protected abstract void ApplyEffect(in GameObject appliedObj);

}
