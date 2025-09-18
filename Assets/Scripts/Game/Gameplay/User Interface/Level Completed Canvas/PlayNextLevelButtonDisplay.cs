using UnityEngine;
using UnityEngine.UI;

public class PlayNextLevelButtonDisplay : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Button playNextLevelButton;

    [Header("Emitting Events")]
    public VoidEventChannel PlayNextLevelButtonPressed;

    [Header("Runtime Sets")]
    public LevelLoadRequestRuntimeSet LevelLoadRequestRuntimeSet;
    public AvailableLevelDataRuntimeSet AvailableLevelDataRuntimeSet;

    private LevelDatum nextLevelDatum;

    void OnEnable()
    {
        int nextLevelID = LevelLoadRequestRuntimeSet.GetLevelDatum().ID + 1;
        if (AvailableLevelDataRuntimeSet.IsLoaded && AvailableLevelDataRuntimeSet.TryGetLevelDatum(nextLevelID, out nextLevelDatum))
        {
            playNextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            playNextLevelButton.gameObject.SetActive(false);

        }
        playNextLevelButton.onClick.AddListener(OnPlayNextLevelButtonPressed);
    }

    void OnDisable()
    {
        playNextLevelButton.onClick.RemoveListener(OnPlayNextLevelButtonPressed);
    }

    private void OnPlayNextLevelButtonPressed()
    {
        LevelLoadRequestRuntimeSet.SetLevelDatum(nextLevelDatum);
        PlayNextLevelButtonPressed.Emit();
    }
}
