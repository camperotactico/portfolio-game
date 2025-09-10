using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelSelectionEventChannel", menuName = "Scriptable Objects/Event Channels/Level Selection Event Channel")]
public class LevelSelectionEventChannel : ScriptableObject
{
    public UnityEvent<int> LevelButtonPressed = new UnityEvent<int>();

    public void EmitLevelButtonPressed(int levelNumber)
    {
        LevelButtonPressed?.Invoke(levelNumber);
    }
}