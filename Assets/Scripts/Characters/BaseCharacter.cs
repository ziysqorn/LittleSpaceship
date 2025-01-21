using UnityEngine;
using GlobalAccess;

public class BaseCharacter : MonoBehaviour
{
    //Properties
    protected int MaxHealth;
	protected int CurrentHealth;
	protected int Damage;

	//Booleans
	protected bool bCanBeDamaged;


    //Components
    protected BoxCollider2D boxCollider;
    //protected Rigidbody2D rigidBody;

	void Awake()
	{
        //Component init
        boxCollider = GetComponent<BoxCollider2D>();
        //rigidBody = GetComponent<Rigidbody2D>();

        //Properties init
		bCanBeDamaged = true;
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
