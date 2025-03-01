using UnityEngine;

public class NormalNautoranWave : NormalWave
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (prefData)
        {
			usedPrefList = prefData.nautoranPrefs;
            SpawnEnemyWave();
		}
    }

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

}
