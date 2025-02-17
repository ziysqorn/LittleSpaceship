using UnityEngine;
using GlobalAccess;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Interfaces;

public class MainCharacter : BaseCharacter
{
    protected float Speed;
	protected float AttackSpeed;
    protected AttackComponent attackComp;
    [SerializeField] protected GameObject currentProjectile;
	void Awake()
    {
        MaxHealth = GameConstants.Max_MainCharater_Health;
        CurrentHealth = MaxHealth;
        Damage = GameConstants.MainCharacter_Damage;
        Speed = GameConstants.MainCharacter_Speed;
        AttackSpeed = GameConstants.MainCharacter_AttackSpeed;

        IShootMode shootMode = new SingleShot();
        IAttackStrategy attackStrategy = new NormalShooting(gameObject, shootMode, currentProjectile);
        attackComp = new AttackComponent(gameObject, attackStrategy);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(attackComp?.AttackLoop(AttackSpeed));
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
