using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] protected PrefabData prefData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameProgress gameProgress = GameplayStatics.LoadGame<GameProgress>("GameProgress.space");
        if(gameProgress != null)
        {
            if (prefData) Instantiate(prefData.savedMainMenuPref);
        }
        else if (prefData)
        {
			Instantiate(prefData.mainMenuPref);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
