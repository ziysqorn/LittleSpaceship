using UnityEngine;
using GlobalAccess;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Interfaces;

public class MainCharacter : BaseCharacter
{
    protected float Speed;
    protected AttackComponent attackComp;
	protected override void Awake()
    {
        base.Awake();
        MaxHealth = GameConstants.Max_MainCharater_Health;
        CurrentHealth = MaxHealth;
        Speed = GameConstants.MainCharacter_Speed;

        attackComp = gameObject.GetComponent<AttackComponent>();
        if (attackComp) { 
            attackComp.SetAttackSpeed(GameConstants.MainCharacter_AttackSpeed);
            attackComp.SetDamage(GameConstants.MainCharacter_Damage);
		} 

	}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        attackComp?.Attack();
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
}
