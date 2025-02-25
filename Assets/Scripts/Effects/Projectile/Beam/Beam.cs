using UnityEngine;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using Game.Interfaces;
using System.Collections;
using UnityEditor.EditorTools;
using static UnityEngine.GraphicsBuffer;

public class Beam : Projectile
{
    protected LineRenderer lineRenderer;
    [SerializeField] protected List<Texture> textures = new List<Texture>();
    protected int animStep = 0;
    protected float lineDistance = 1000.0f;
	protected override void Awake()
	{
		base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
		bulletDamage = 2;
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Start()
    {
        base.Start();
        StartCoroutine(Burn());
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer && textures.Count > 0)
        {
			++animStep;
            if(animStep >= textures.Count) animStep %= textures.Count;
            lineRenderer.material.mainTexture = textures[animStep];

            ApplyLine();
        }
    }

	public void ApplyLine()
    {
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition(0, gameObject.transform.position);
		Vector3 endPos = gameObject.transform.position + gameObject.transform.up * lineDistance;
		LayerMask layerMask = LayerMask.GetMask("Character");
		RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(gameObject.transform.position, gameObject.transform.up, lineDistance, layerMask);
		foreach (RaycastHit2D hit in raycastHit2D)
		{
			if (hit && hit.collider)
			{
				GameObject otherObj = hit.collider.gameObject;
				if (otherObj && owner && otherObj.tag != owner.tag)
				{
					endPos = hit.point;
					break;
				}
			}
		}
		lineRenderer.SetPosition(1, endPos);
	}

    protected IEnumerator Burn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            LayerMask layerMask = LayerMask.GetMask("Character");
			RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(gameObject.transform.position, gameObject.transform.up, lineDistance, layerMask);
			foreach (RaycastHit2D hit in raycastHit2D)
			{
				if (hit && hit.collider)
				{
					GameObject otherObj = hit.collider.gameObject;
					if (otherObj && owner && otherObj.tag != owner.tag)
					{
						PoolManager poolManager = PoolManager.poolManager;
						if (explosion)
						{
							Explosion attachedExplosion = explosion.GetComponent<Explosion>();
							if (attachedExplosion && attachedExplosion.effectName != "")
							{
								if (!poolManager.PoolExisted(attachedExplosion.effectName)) poolManager.RegisterPool(attachedExplosion.effectName, new ObjectPool(explosion));
								poolManager.ActivateObjFromPool(attachedExplosion.effectName, hit.point, gameObject.transform.rotation);
							}
							AttackComponent attackComp = owner.GetComponent<AttackComponent>();
							if (attackComp) GameplayStatics.ApplyDamage(otherObj, bulletDamage + attackComp.Damage, owner, gameObject);
						}
						break;
					}
				}
			}
		}
	}
}
