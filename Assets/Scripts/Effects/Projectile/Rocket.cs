using UnityEngine;

public class Rocket : Projectile
{
    protected Rigidbody2D rBody;
    [SerializeField] protected GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        if (owner) rBody.linearVelocityY = owner.transform.up.y * 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
