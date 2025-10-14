using System.Collections;
using System.Collections.Generic;
using Game.Scriptable_Objects.Data.Levels;
using UnityEngine;

public class TestLevelDataProvider : BaseLevelDataProvider
{
    [Header("Parameters")]
    [SerializeField]
    private LevelDataCollection testLevelData;

    public override IEnumerator LoadLevelData()
    {
        availableLevelDataRuntimeSet.AddLevelData(testLevelData);
        yield return new WaitForSeconds(1f);
        Debug.Log("Test Levels loaded");
        yield return null;
    }
}
