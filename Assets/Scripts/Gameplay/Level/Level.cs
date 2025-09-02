using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("Level Parameters")]
    public LevelDatum LevelDatum;

    [Header("Emitting Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;


    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;

        levelLifecycleEventChannel.EmitInitialisationRequestedEvent(LevelDatum);
        levelLifecycleEventChannel.EmitStartedEvent();
    }
}