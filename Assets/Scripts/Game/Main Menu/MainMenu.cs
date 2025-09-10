using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    [SerializeField]
    private LevelDataAvailabilityEventChannel levelDataAvailabilityEventChannel;

    void Start()
    {
        levelDataAvailabilityEventChannel.EmitLevelDataRequested();
    }

}
