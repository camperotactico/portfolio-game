using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreTrackerEventChannel", menuName = "Scriptable Objects/Event Channels/Score Tracker Event Channel")]
public class ScoreTrackerEventChannel : ScriptableObject
{
    public UnityEvent<int> CompletionScoreChanged = new UnityEvent<int>();
    public UnityEvent<int> CurrentScoreChanged = new UnityEvent<int>();
    public UnityEvent<int> LevelScoringFinished = new UnityEvent<int>();

    internal void EmitCompletionScoreChanged(int completionScore)
    {
        CompletionScoreChanged?.Invoke(completionScore);
    }

    internal void EmitCurrentScoreChanged(int currentScore)
    {
        CurrentScoreChanged?.Invoke(currentScore);
    }

    internal void EmitLevelScoringFinished(int finishScore)
    {
        LevelScoringFinished?.Invoke(finishScore);
    }
}