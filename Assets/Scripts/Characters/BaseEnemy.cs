using GlobalAccess;
using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
	void Awake()
	{
        AttackSpeed = GameConstants.Enemy_AttackSpeed;
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
