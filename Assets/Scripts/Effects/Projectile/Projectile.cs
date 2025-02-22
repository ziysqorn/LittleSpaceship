using Game.Interfaces;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projectileName = "";
    protected int bulletDamage;
    public GameObject owner;
	[SerializeField] protected GameObject explosion;
	protected virtual void Awake()
    {
        bulletDamage = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
