using System;
using Game.Scriptable_Objects.Data.Levels;
using UnityEngine.AddressableAssets;

namespace Game.Main_Menu.Level_Data_Provider.AddressableLevelDataProvider
{
    [Serializable]
    public class AssetReferenceLevelDataCollection : AssetReferenceT<LevelDataCollection>
    {
        public AssetReferenceLevelDataCollection(string guid) : base(guid)
        {
        }
    }
}