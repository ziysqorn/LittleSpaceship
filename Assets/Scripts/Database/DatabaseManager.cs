using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    public CombatDatabase combatDb;
    public QuizDatabase quizDb;
    [SerializeField] PrefabData prefData;

	void Awake()
	{
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		if (instance == null)
		{
			instance = this;
			QuizServices quizServices = GetComponent<QuizServices>();
			combatDb = new CombatDatabase(prefData);
			quizDb = new QuizDatabase(quizServices);
			DontDestroyOnLoad(gameObject);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
