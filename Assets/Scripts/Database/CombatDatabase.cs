using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatDatabase
{
	Dictionary<string, Type> atkStrategyMap = new Dictionary<string, Type>();
	Dictionary<string, Type> shootingModeMap = new Dictionary<string, Type>();	

	public CombatDatabase()
	{
		atkStrategyMap.Add("NormalShooting", typeof(NormalShooting));
		atkStrategyMap.Add("BeamShooting", typeof(BeamShooting));
		shootingModeMap.Add("SingleShot", typeof(SingleShot));
		shootingModeMap.Add("DoubleShot", typeof(DoubleShot));
		shootingModeMap.Add("TripleShot", typeof(TripleShot));
		shootingModeMap.Add("SingleBeam", typeof(SingleBeam));
		shootingModeMap.Add("DoubleBeam", typeof(DoubleBeam));
		shootingModeMap.Add("TripleBeam", typeof(TripleBeam));
	}
}
