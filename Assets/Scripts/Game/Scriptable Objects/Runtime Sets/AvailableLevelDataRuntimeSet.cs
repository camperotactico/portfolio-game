
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableLevelDataRuntimeSet", menuName = "Scriptable Objects/Runtime Sets/Available Level Data Runtime Set")]
public class AvailableLevelDataRuntimeSet : ScriptableObject
{
    public List<LevelDatum> AvailableLevelData = new List<LevelDatum>();

}