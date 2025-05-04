using UnityEngine;

public class CollectiblesCyberBadge : Collectibles
{
	[SerializeField] protected int point;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        point = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected override void ApplyEffect(in GameObject appliedObj)
    {
		MainSceneScript mainScene = FindFirstObjectByType<MainSceneScript>();
		if (mainScene)
		{
			mainScene.UpdateScore(point);
		}
	}
}
