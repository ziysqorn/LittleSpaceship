using UnityEngine;

public class NormalRocket : Rocket
{
	protected override void Awake()
	{
		base.Awake();
        bulletDamage = 0;
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
