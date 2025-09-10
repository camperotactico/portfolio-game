using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [Header("Emitting Event Channels")]
    [SerializeField]
    private LevelSelectionEventChannel levelSelectionEventChannel;

    public void OnButtonPressed()
    {
        levelSelectionEventChannel.EmitLevelButtonPressed(0);
    }
}
