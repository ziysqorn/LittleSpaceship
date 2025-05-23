using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatDatabase
{
	Dictionary<string, KeyValuePair<Type, GameObject>> atkStrategyMap = new Dictionary<string, KeyValuePair<Type, GameObject>>();

	public CombatDatabase()
	{

	}

	public CombatDatabase(in PrefabData prefData)
	{
		if (prefData && prefData.normalRocketPref && prefData.armorPiercePref && prefData.laserBeamPref)
		{
			atkStrategyMap.Add("NormalRocket", new KeyValuePair<Type, GameObject>(typeof(NormalShooting), prefData.normalRocketPref));
			atkStrategyMap.Add("ArmorPierce", new KeyValuePair<Type, GameObject>(typeof(NormalShooting), prefData.armorPiercePref));
			atkStrategyMap.Add("LaserBeam", new KeyValuePair<Type, GameObject>(typeof(BeamShooting), prefData.laserBeamPref));
		}
	}

	public KeyValuePair<Type, GameObject>? getAtkStrategy(in string inStrategyName)
	{
		if (atkStrategyMap.ContainsKey(inStrategyName))
		{
			return atkStrategyMap[inStrategyName];
		}
		return null;
	}
}
