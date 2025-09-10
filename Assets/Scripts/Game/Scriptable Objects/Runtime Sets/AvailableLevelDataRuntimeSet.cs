
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableLevelDataRuntimeSet", menuName = "Scriptable Objects/Runtime Sets/Available Level Data Runtime Set")]
public class AvailableLevelDataRuntimeSet : ScriptableObject
{
    private ICollection<LevelDatum> availableLevelData = new HashSet<LevelDatum>();


    public void ClearAvailableLevelData()
    {
        availableLevelData.Clear();
    }

    public void AddAvailableLevelData(ICollection<LevelDatum> newLevelDataToAdd)
    {
        foreach (LevelDatum levelDatum in newLevelDataToAdd)
        {
            if (!availableLevelData.Contains(levelDatum))
            {
                availableLevelData.Add(levelDatum);
            }
        }
    }

    public ICollection<LevelDatum> GetAvailableLevelData()
    {
        return availableLevelData;
    }
}