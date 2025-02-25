using UnityEngine;

public class NautoranDreadnought : Nautoran
{
	protected override void Awake()
	{
		base.Awake();
		fightingStyle = new Dreadnought();
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
