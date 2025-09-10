using System.Collections;
using UnityEngine;

public abstract class BaseLevelDataProvider : MonoBehaviour
{
    [Header("Emitting & Receiving Event Channels")]
    [SerializeField]
    private LevelDataAvailabilityEventChannel levelDataAvailabilityEventChannel;

    [Header("Runtime Sets")]
    [SerializeField]
    protected AvailableLevelDataRuntimeSet availableLevelDataRuntimeSet;

    private Coroutine loadLevelDataCoroutine;

    void OnEnable()
    {
        levelDataAvailabilityEventChannel.LevelDataRequested.AddListener(OnLeveDataRequested);
    }
    void OnDisable()
    {
        levelDataAvailabilityEventChannel.LevelDataRequested.RemoveListener(OnLeveDataRequested);
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
        yield return LoadLevelData();
        levelDataAvailabilityEventChannel.EmitLevelDataReady();
        yield return null;
    }

    public abstract IEnumerator LoadLevelData();
}