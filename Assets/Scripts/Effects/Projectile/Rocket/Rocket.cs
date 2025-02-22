using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Rocket : Projectile
{
    protected float flightSpeed = 8.0f;
    protected Rigidbody2D rBody;
    protected BoxCollider2D boxCollider;
	protected override void Awake()
	{
		base.Awake();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnEnable()
	{
        if(rBody == null) rBody = GetComponent<Rigidbody2D>();
        if(boxCollider == null) boxCollider = GetComponent<BoxCollider2D>();
		rBody.linearVelocity = transform.up * flightSpeed;
	}

	protected void OnBecameInvisible()
	{
        PoolManager manager = PoolManager.poolManager;
        if (manager != null) { 
            manager.RetrieveObjToPool(projectileName, gameObject);
        }
	}
}
