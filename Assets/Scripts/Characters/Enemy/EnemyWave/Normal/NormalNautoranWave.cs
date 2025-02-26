using UnityEngine;

public class NormalNautoranWave : NormalWave
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(prefData) usedPrefList = prefData.nautoranPrefs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
