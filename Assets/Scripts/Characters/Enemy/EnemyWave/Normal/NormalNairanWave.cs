using UnityEngine;

public class NormalNairanWave : NormalWave
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
	{
		base.Start();
		if (prefData) usedPrefList = prefData.nairanPrefs;
        SpawnEnemyWave();
    }

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

}
