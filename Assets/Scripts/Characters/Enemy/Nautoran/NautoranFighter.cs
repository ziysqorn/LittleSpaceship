using UnityEngine;

public class NautoranFighter : Nautoran
{
	protected override void Awake()
	{
		base.Awake();
		fightingStyle = new Fighter();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
