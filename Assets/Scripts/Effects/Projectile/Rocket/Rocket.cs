using UnityEngine;

public class Rocket : Projectile
{
    protected Rigidbody2D rBody;
    [SerializeField] protected GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.linearVelocity = transform.up * 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
