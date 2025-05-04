using System.Collections;
using UnityEngine;

public class CollectiblesCyberDefender : Collectibles
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	protected override void ApplyEffect(in GameObject appliedObj)
    {
        MainCharacter mainCharacter = appliedObj.GetComponent<MainCharacter>();
        if (mainCharacter)
        {
            AttackComponent attackComponent = mainCharacter.GetComponent<AttackComponent>();
			if (attackComponent)
			{
                RangedAttack rangedAttack = attackComponent.strategy as RangedAttack;
				if (rangedAttack != null)
				{
                    rangedAttack.SetCurMode(2);
                    rangedAttack.UpdateCurMode();
                    rangedAttack.Attack();
					attackComponent.StartCoroutine(StopEffect(5.0f, appliedObj));
				}
			}
		}
    }

	public IEnumerator StopEffect(float delay, GameObject appliedObj)
	{
		yield return new WaitForSeconds(delay);
		MainCharacter mainCharacter = appliedObj.GetComponent<MainCharacter>();
		if (mainCharacter)
		{
			AttackComponent attackComponent = mainCharacter.GetComponent<AttackComponent>();
			if (attackComponent)
			{
				RangedAttack rangedAttack = attackComponent.strategy as RangedAttack;
				if (rangedAttack != null)
				{
					rangedAttack.SetCurMode(1);
					rangedAttack.UpdateCurMode();
					rangedAttack.Attack();
				}
			}
		}
	}

}
