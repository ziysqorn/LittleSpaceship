using UnityEngine;

public class NormalKlaedWave : NormalWave
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(prefData) usedPrefList = prefData.klaedPrefs;
        SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
