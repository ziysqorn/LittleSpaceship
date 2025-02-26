using Game.Interfaces;
using UnityEngine;

public class CollectibleArmorPierce : CollectibleProc
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected void OnEnable()
	{
		SetPrefabName();
	}

	protected override void ApplyEffect(in GameObject appliedObj)
	{
		AttackComponent attackComp = appliedObj.GetComponent<AttackComponent>();
        if (attackComp != null)
        {
            RangedAttack rangedAttack = attackComp.strategy as RangedAttack;
			attackComp.StopAllCoroutines();
			Projectile proc = projectile.GetComponent<Projectile>();
			int curMode = rangedAttack != null ? rangedAttack.IncreaseCurMode(proc.projectileName) : 1;
			if(curMode == 1) rangedAttack = new NormalShooting(appliedObj, projectile);
			for (int i = 0; i < appliedObj.transform.childCount;)
			{
				GameObject child = appliedObj.transform.GetChild(i).gameObject;
				Beam beam = child.GetComponent<Beam>();
				if (beam)
				{
					child.transform.parent = null;
					PoolManager manager = PoolManager.poolManager;
					if (manager && beam.projectileName != "") manager.RetrieveObjToPool(beam.projectileName, child);
				}
				else ++i;
			}
			rangedAttack.SetCurMode(curMode);
			rangedAttack.UpdateCurMode();
			attackComp.SetStrategy(rangedAttack);
			attackComp.Attack();
		}
	}

	public void SetPrefabName()
	{
		collectibleName = "Collectible_ArmorPierce";
	}

	public string GetPrefabName()
	{
		return collectibleName;
	}
}
