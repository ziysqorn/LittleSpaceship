using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
	[SerializeField] protected AudioSource backgroundSource;
	[SerializeField] protected AudioSource SFXSource;
	[SerializeField] protected AudioClip backgroundMusic;
	[SerializeField] public AudioClip SFX_shipExplosion;
	[SerializeField] public AudioClip SFX_congrats;
	[SerializeField] public AudioClip SFX_gameOver;
	[SerializeField] public AudioClip SFX_ticktock;
	[SerializeField] public AudioClip SFX_rewarded;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		if (backgroundSource && backgroundMusic)
		{
			backgroundSource.clip = backgroundMusic;
			backgroundSource.loop = true;
			backgroundSource.volume = 0.5f;
			backgroundSource.Play();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void PlaySFX(AudioClip clip)
	{
		if (clip) SFXSource.PlayOneShot(clip);
	}
}
