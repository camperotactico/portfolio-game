using System;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    private const string SCORE_FORMAT_TEMPLATE = "F1";

    [Header("Components")]
    [SerializeField]
    private TMP_Text remainingTimeText;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private GameTimerEventChannel gameTimerEventChannel;

    void OnEnable()
    {
        gameTimerEventChannel.RemainingTimeChanged.AddListener(OnRemainingTimeChanged);
    }

    void OnDisable()
    {
        gameTimerEventChannel.RemainingTimeChanged.RemoveListener(OnRemainingTimeChanged);
    }

    private void OnRemainingTimeChanged(float newRemainingTime)
    {
        remainingTimeText.text = newRemainingTime.ToString(SCORE_FORMAT_TEMPLATE);
    }
}
