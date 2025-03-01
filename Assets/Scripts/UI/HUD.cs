using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txt_Score;
    [SerializeField] protected GameObject panel;
    [SerializeField] protected PrefabData prefData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		ScoreboardSave scoreboardSave = GameplayStatics.LoadGame<ScoreboardSave>("ScoreboardSave.space");
		if (scoreboardSave != null)
		{
			txt_Score.text = scoreboardSave.curScore.ToString();
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        txt_Score.text = score.ToString();
    }

    public void DecreaseHeart()
    {
        if (panel)
        {
            int childNum = panel.transform.childCount;
            if(childNum >= 0)
            {
                Destroy(panel.transform.GetChild(childNum - 1).gameObject);
            }
		}
    }

    public void LoadHeart(int heart)
    {
        if (prefData)
        {
			for (int i = 0; i < heart; ++i)
			{
                GameObject newHeart = Instantiate(prefData.heartPref);
                newHeart.transform.SetParent(panel.transform, false);
			}
		}
    }
}
