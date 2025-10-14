using System.Collections;
using Game.Main_Menu.Level_Data_Provider.AddressableLevelDataProvider;
using Game.Scriptable_Objects.Data.Levels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLevelDataProvider : BaseLevelDataProvider
{
    [Header("Parameters")]
    [SerializeField]
    private AssetReferenceLevelDataCollection assetReferenceLevelDataCollection;
    public override IEnumerator LoadLevelData()
    {
       AsyncOperationHandle<LevelDataCollection> asyncOperationHandle = assetReferenceLevelDataCollection.LoadAssetAsync();
       if (!asyncOperationHandle.IsDone)
       {
           yield return asyncOperationHandle;
       }
       if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
       { 
           // TODO: Call Addressables.Release(asyncOperationHandle); on application quit.
           availableLevelDataRuntimeSet.AddLevelData(asyncOperationHandle.Result);
       }
       else
       {
            Addressables.Release(asyncOperationHandle);
       }
       yield return null;
    }
}
