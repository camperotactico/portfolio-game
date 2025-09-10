using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelDataProvider : BaseLevelDataProvider
{
    [Header("Parameters")]
    [SerializeField]
    private List<LevelDatum> testLevelData;

    public override IEnumerator LoadLevelData()
    {
        availableLevelDataRuntimeSet.AddAvailableLevelData(testLevelData);
        yield return new WaitForSeconds(1f);
        Debug.Log("Test Levels loaded");
        yield return null;
    }
}
