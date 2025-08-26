using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private const string SCORE_FORMAT_TEMPLATE = "D7";

    [SerializeField]
    private TMP_Text scoreNumberText;

    private void OnEnable()
    {
        ScoreTracker.CurrentScoreChanged += OnCurrentScoreChanged;
    }

    private void OnDisable()
    {
        ScoreTracker.CurrentScoreChanged -= OnCurrentScoreChanged;
    }

    private void OnCurrentScoreChanged(int newCurrentScore)
    {
        scoreNumberText.text = newCurrentScore.ToString(SCORE_FORMAT_TEMPLATE);
    }
}
