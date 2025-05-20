using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "Scriptable Objects/PrefabData")]
public class PrefabData : ScriptableObject
{
    public List<GameObject> collectibleProcPrefs;
    public List<GameObject> klaedPrefs;
	public List<GameObject> nairanPrefs;
	public List<GameObject> nautoranPrefs;
	public List<GameObject> enemyWavesPrefs;
	public GameObject normalRocketPref;
	public GameObject shieldPref;
	public GameObject mainMenuPref;
	public GameObject savedMainMenuPref;
	public GameObject pauseMenuPref;
	public GameObject tryAgainMenuPref;
	public GameObject congratsMenuPref;
	public GameObject quizDialogWindowPref;
	public GameObject screenMessagePref;
	public GameObject heartPref;
	public GameObject upgradeWindowPref;
	public GameObject dialogBoxPref;
}
