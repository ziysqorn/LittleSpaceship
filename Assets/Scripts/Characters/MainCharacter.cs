using UnityEngine;
using GlobalAccess;
using System.Collections;

public class MainCharacter : BaseCharacter
{
    protected float Speed;
    [SerializeField] protected GameObject rocketPrefab;
	[SerializeField] protected GameObject shieldPrefab;
    void Awake()
    {
        MaxHealth = GameConstants.Max_MainCharater_Health;
        CurrentHealth = MaxHealth;
        Damage = GameConstants.MainCharacter_Damage;
        Speed = GameConstants.MainCharacter_Speed;
        AttackSpeed = GameConstants.MainCharacter_AttackSpeed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(AttackAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
	}

    protected void Move(){
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		mousePos.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, mousePos, Speed * Time.deltaTime);
	}

    protected void Attack()
    {
        if (rocketPrefab)
        {
            GameObject prefab = Instantiate(rocketPrefab);
            if (prefab)
            {
                Rocket rocket = prefab.GetComponent<Rocket>();
				if (rocket)
                {
                    rocket.owner = gameObject;
                }
			}

        }
    }

    IEnumerator AttackAfterDelay()
    {
        yield return new WaitForSeconds(AttackSpeed);
		Attack();
    }
}
