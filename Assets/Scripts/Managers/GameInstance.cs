using UnityEngine;
using GlobalAccess;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance {  get; private set; }

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
