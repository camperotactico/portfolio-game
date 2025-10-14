using UnityEngine;

namespace Game.Scriptable_Objects.Data.Levels
{
    [CreateAssetMenu(fileName = "Level Data Collection", menuName = "Scriptable Objects/Data/Levels/Level Data Collection", order = 0)]
    public class LevelDataCollection : ScriptableObject
    {
        public LevelDatum[] levelData;
    }
}