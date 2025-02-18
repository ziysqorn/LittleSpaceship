using UnityEngine;

public class Beam : Projectile
{
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected RaycastHit2D raycastHit2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
