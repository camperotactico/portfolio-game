using UnityEngine;

[CreateAssetMenu(fileName = "LevelLoadRequestRuntimeSet", menuName = "Scriptable Objects/Runtime Sets/Level Load Request Runtime Set")]
public class LevelLoadRequestRuntimeSet : ScriptableObject
{
    [SerializeField]
    private LevelDatum levelDatum;

    public void SetLevelDatum(LevelDatum newLevelDatum)
    {
        levelDatum = newLevelDatum;
    }

    public LevelDatum GetLevelDatum()
    {
        return levelDatum;
    }

}
