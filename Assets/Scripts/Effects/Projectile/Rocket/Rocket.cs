using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Rocket : Projectile
{
    protected float flightSpeed = 8.0f;
    protected Rigidbody2D rBody;
    [SerializeField] protected GameObject explosion;
	protected override void Awake()
	{
		base.Awake();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
        base.Start();
        rBody = GetComponent<Rigidbody2D>();
        rBody.linearVelocity = transform.up * flightSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnBecameInvisible()
	{
        PoolManager manager = PoolManager.poolManager;
        if (manager != null) { 
            manager.RetrieveObjToPool("Projectile", gameObject);
        }
	}
}
