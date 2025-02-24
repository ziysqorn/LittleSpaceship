using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "Scriptable Objects/PrefabData")]
public class PrefabData : ScriptableObject
{
    public List<GameObject> collectibleProcPrefs;
}
