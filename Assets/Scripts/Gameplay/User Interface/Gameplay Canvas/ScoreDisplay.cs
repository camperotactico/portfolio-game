using System;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private const string SCORE_FORMAT_TEMPLATE = "D7";

    [Header("Components")]
    [SerializeField]
    private TMP_Text completionScoreNumberText;
    [SerializeField]
    private TMP_Text currentScoreNumberText;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private ScoreTrackerEventChannel scoreTrackerEventChannel;

    private void OnEnable()
    {
        scoreTrackerEventChannel.CurrentScoreChanged.AddListener(OnCurrentScoreChanged);
        scoreTrackerEventChannel.CompletionScoreChanged.AddListener(OnCompletionScoreChanged);
    }

    private void OnDisable()
    {
        scoreTrackerEventChannel.CurrentScoreChanged.RemoveListener(OnCurrentScoreChanged);
        scoreTrackerEventChannel.CompletionScoreChanged.RemoveListener(OnCompletionScoreChanged);
    }

    private void OnCompletionScoreChanged(int newCompletionScore)
    {
        completionScoreNumberText.text = newCompletionScore.ToString(SCORE_FORMAT_TEMPLATE);
    }

    private void OnCurrentScoreChanged(int newCurrentScore)
    {
        currentScoreNumberText.text = newCurrentScore.ToString(SCORE_FORMAT_TEMPLATE);
    }
}
