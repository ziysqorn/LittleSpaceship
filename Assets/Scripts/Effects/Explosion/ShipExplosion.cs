using Game.Interfaces;
using UnityEngine;

public class ShipExplosion : Explosion, INameablePrefab
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrefabName()
    {
        effectName = "ShipExplosion";
    }

	public string GetPrefabName()
	{
		return effectName;
	}
}
