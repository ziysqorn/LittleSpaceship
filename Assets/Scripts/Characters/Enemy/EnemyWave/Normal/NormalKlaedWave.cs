using UnityEngine;

public class NormalKlaedWave : NormalWave
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
	{
		base.Start();
		if (prefData) usedPrefList = prefData.klaedPrefs;
        SpawnEnemyWave();
    }

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

}
