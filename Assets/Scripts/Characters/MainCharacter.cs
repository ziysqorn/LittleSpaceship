using UnityEngine;
using GlobalAccess;

public class MainCharacter : BaseCharacter
{
    protected float Speed;
	void Awake()
	{
        MaxHealth = GameConstants.Max_MainCharater_Health;
        CurrentHealth = MaxHealth;
        Damage = GameConstants.MainCharacter_Damage;
        Speed = GameConstants.MainCharacter_Speed;
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, mousePos, Speed * Time.deltaTime);
	}
}
