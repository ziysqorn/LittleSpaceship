using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void UpdateHeart(bool isIncreased)
    {
        if (panel)
        {
            if (!isIncreased)
            {
				int childNum = panel.transform.childCount;
				if (childNum >= 0)
				{
					GameObject lastChild = panel.transform.GetChild(childNum - 1).gameObject;
					lastChild.transform.SetParent(null, false);
					Destroy(lastChild);
				}
			}
            else
            {
				GameObject newHeart = Instantiate(prefData.heartPref);
				newHeart.transform.SetParent(panel.transform, false);
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
