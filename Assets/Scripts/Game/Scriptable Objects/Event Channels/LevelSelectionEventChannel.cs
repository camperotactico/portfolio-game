using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelSelectionEventChannel", menuName = "Scriptable Objects/Event Channels/Level Selection Event Channel")]
public class LevelSelectionEventChannel : ScriptableObject
{
    public UnityEvent<LevelDatum> LevelButtonPressed = new UnityEvent<LevelDatum>();

    public void EmitLevelButtonPressed(LevelDatum levelDatum)
    {
        LevelButtonPressed?.Invoke(levelDatum);
    }
}