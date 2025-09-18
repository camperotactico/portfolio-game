using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    private const string SCORE_FORMAT_TEMPLATE = "F1";

    [Header("Components")]
    [SerializeField]
    private TMP_Text remainingTimeText;

    [Header("Receiving Event Channels")]
    public FloatEventChannel GameTimerRemainingTimeChanged;

    void OnEnable()
    {
        GameTimerRemainingTimeChanged.AddListener(OnRemainingTimeChanged);
    }

    void OnDisable()
    {
        GameTimerRemainingTimeChanged.RemoveListener(OnRemainingTimeChanged);
    }

    private void OnRemainingTimeChanged(float newRemainingTime)
    {
        remainingTimeText.text = newRemainingTime.ToString(SCORE_FORMAT_TEMPLATE);
    }
}
