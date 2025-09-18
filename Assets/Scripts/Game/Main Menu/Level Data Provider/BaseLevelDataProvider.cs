using System.Collections;
using UnityEngine;

public abstract class BaseLevelDataProvider : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    public VoidEventChannel LevelDataReady;

    [Header("Receiving Event Channels")]
    public VoidEventChannel LevelDataRequested;

    [Header("Runtime Sets")]
    [SerializeField]
    protected AvailableLevelDataRuntimeSet availableLevelDataRuntimeSet;

    private Coroutine loadLevelDataCoroutine;


    void OnEnable()
    {
        LevelDataRequested.AddListener(OnLeveDataRequested);
    }
    void OnDisable()
    {
        LevelDataRequested.RemoveListener(OnLeveDataRequested);
    }

    private void OnLeveDataRequested()
    {
        if (loadLevelDataCoroutine != null)
        {
            Debug.LogError("Trying to load LevelData twice.");
            return;
        }
        loadLevelDataCoroutine = StartCoroutine(WaitForLevelDataLoad());
    }

    private IEnumerator WaitForLevelDataLoad()
    {
        if (!availableLevelDataRuntimeSet.IsLoaded)
        {
            yield return LoadLevelData();
            availableLevelDataRuntimeSet.IsLoaded = true;
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }
        LevelDataReady.Emit();
        yield return null;
    }

    public abstract IEnumerator LoadLevelData();
}