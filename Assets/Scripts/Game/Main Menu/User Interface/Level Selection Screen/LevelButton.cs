using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    private const string LEVEL_NUMBER_TEXT_TEMPLATE = "Level {0}";

    [Header("Emitting Event Channels")]
    public LevelDatumEventChannel LevelButtonPressed;

    [Header("Components")]
    [SerializeField]
    private TMP_Text levelNumberText;

    private LevelDatum levelDatum;

    public void OnButtonPressed()
    {
        LevelButtonPressed.Emit(levelDatum);
    }

    internal void SetLevelDatum(LevelDatum newLevelDatum)
    {
        levelDatum = newLevelDatum;
        levelNumberText.SetText(LEVEL_NUMBER_TEXT_TEMPLATE, levelDatum.ID);
    }
}
