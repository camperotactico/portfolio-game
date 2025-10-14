
using System.Collections.Generic;
using Game.Scriptable_Objects.Data.Levels;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableLevelDataRuntimeSet", menuName = "Scriptable Objects/Runtime Sets/Available Level Data Runtime Set")]
public class AvailableLevelDataRuntimeSet : ScriptableObject
{
    public bool IsLoaded = false;
    public int LevelCount { get => availableLevelData.Count; }
    [SerializeField]
    private List<LevelDatum> availableLevelData = new List<LevelDatum>();
    private IDictionary<int, LevelDatum> levelIDToLevelDatum = new Dictionary<int, LevelDatum>();


    void OnEnable()
    {
        Application.quitting += OnApplicationQuit;
    }

    void OnDisable()
    {
        Application.quitting -= OnApplicationQuit;
    }

    private void OnApplicationQuit()
    {
        IsLoaded = false;
        availableLevelData.Clear();
        levelIDToLevelDatum.Clear();
    }
    
    public void AddLevelData(LevelDataCollection newLevelDataCollection)
    {
        AddLevelData(newLevelDataCollection.levelData);
    }

    public void AddLevelData(ICollection<LevelDatum> newLevelData)
    {
        availableLevelData.AddRange(newLevelData);
        foreach (LevelDatum levelDatum in newLevelData)
        {
            levelIDToLevelDatum[levelDatum.ID] = levelDatum;
        }
    }

    public bool TryGetLevelDatum(int levelID, out LevelDatum levelDatum)
    {
        return levelIDToLevelDatum.TryGetValue(levelID, out levelDatum);
    }
}