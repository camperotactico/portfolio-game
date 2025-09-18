using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    public VoidEventChannel LevelDataRequested;

    void Start()
    {
        LevelDataRequested.Emit();
    }

}
