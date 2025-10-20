using System.Collections;
using System.Collections.Generic;
using Game.Scriptable_Objects.Data.Levels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLevelDataProvider : BaseLevelDataProvider
{
    [Header("Parameters")]
    [SerializeField]
    private List<string> levelsAddressableLabels = new List<string>() { "Levels"};

    private AsyncOperationHandle<IList<LevelDatum>> _levelsLoadAsyncOperationHandle;
    public override IEnumerator LoadLevelData()
    {
        _levelsLoadAsyncOperationHandle =
            Addressables.LoadAssetsAsync<LevelDatum>(levelsAddressableLabels, null, Addressables.MergeMode.Union);

        if (!_levelsLoadAsyncOperationHandle.IsDone)
        {
            yield return _levelsLoadAsyncOperationHandle;
        }

        if (_levelsLoadAsyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            availableLevelDataRuntimeSet.AddLevelData(_levelsLoadAsyncOperationHandle.Result);
            availableLevelDataRuntimeSet.ApplicationQuitting.AddListener(ReleaseLevelsLoadAsyncOperationHandle);
        }
        else
        {
            ReleaseLevelsLoadAsyncOperationHandle();
        }

        yield return null;
    }

    private void ReleaseLevelsLoadAsyncOperationHandle()
    {
        Addressables.Release(_levelsLoadAsyncOperationHandle);
    }
}
