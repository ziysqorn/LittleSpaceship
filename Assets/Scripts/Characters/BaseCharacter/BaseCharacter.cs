using UnityEngine;
using GlobalAccess;
using System.Collections.Generic;

public abstract class BaseCharacter : MonoBehaviour
{
    //Properties
    protected int MaxHealth;
    protected int CurrentHealth;
	protected int Damage;

	//Booleans
	protected bool bCanBeDamaged;


    //Components
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rBody;

	protected virtual void Awake()
	{
        //Component init
        boxCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();

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
