using UnityEngine;
using UnityEngine.UI;

public class LevelButtonsDisplay : MonoBehaviour
{
    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelDataAvailabilityEventChannel levelDataAvailabilityEventChannel;

    [Header("Runtime Sets")]
    [SerializeField]
    private AvailableLevelDataRuntimeSet availableLevelDataRuntimeSet;

    [Header("Components")]
    [SerializeField]
    private GameObject loadingLevelDataDisplay;
    [SerializeField]
    private GameObject buttonsDisplay;
    [SerializeField]
    private Transform buttonsParent;
    [SerializeField]
    private Button previousPageButton;
    [SerializeField]
    private Button nextPageButton;

    private LevelButton[] levelButtons;
    private int currentButtonPageIndex;
    private int totalButtonPages;

    void Awake()
    {
        levelButtons = buttonsParent.GetComponentsInChildren<LevelButton>();
    }

    void OnEnable()
    {
        levelDataAvailabilityEventChannel.LevelDataRequested.AddListener(OnLevelDataRequested);
        levelDataAvailabilityEventChannel.LevelDataReady.AddListener(OnLevelDataReady);
        previousPageButton.onClick.AddListener(OnPreviousPageButtonPressed);
        nextPageButton.onClick.AddListener(OnNextPageButtonPressed);
    }

    void OnDisable()
    {
        levelDataAvailabilityEventChannel.LevelDataRequested.RemoveListener(OnLevelDataRequested);
        levelDataAvailabilityEventChannel.LevelDataReady.RemoveListener(OnLevelDataReady);
        previousPageButton.onClick.RemoveListener(OnPreviousPageButtonPressed);
        nextPageButton.onClick.RemoveListener(OnNextPageButtonPressed);
    }

    private void OnLevelDataRequested()
    {
        loadingLevelDataDisplay.SetActive(true);
        buttonsDisplay.SetActive(false);
    }

    private void OnLevelDataReady()
    {
        loadingLevelDataDisplay.SetActive(false);
        buttonsDisplay.SetActive(true);

        totalButtonPages = availableLevelDataRuntimeSet.AvailableLevelData.Count / levelButtons.Length;
        if (availableLevelDataRuntimeSet.AvailableLevelData.Count % levelButtons.Length > 0)
        {
            totalButtonPages++;
        }

        SetCurrentPageIndex(0);
    }

    private void SetCurrentPageIndex(int newCurrentPageIndex)
    {
        currentButtonPageIndex = newCurrentPageIndex;
        UpdatePageNavigationButtonsVisibility();
        UpdateDisplayedLevelButtons();
    }

    private void UpdatePageNavigationButtonsVisibility()
    {
        previousPageButton.gameObject.SetActive(currentButtonPageIndex > 0);
        nextPageButton.gameObject.SetActive(currentButtonPageIndex < totalButtonPages - 1);
    }

    public void UpdateDisplayedLevelButtons()
    {
        int startLevelDatumIndex = currentButtonPageIndex * levelButtons.Length;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelDatumIndex = startLevelDatumIndex + i;
            if (levelDatumIndex < availableLevelDataRuntimeSet.AvailableLevelData.Count)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].SetLevelDatum(availableLevelDataRuntimeSet.AvailableLevelData[levelDatumIndex]);
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnPreviousPageButtonPressed()
    {
        SetCurrentPageIndex(currentButtonPageIndex - 1);
    }

    private void OnNextPageButtonPressed()
    {
        SetCurrentPageIndex(currentButtonPageIndex + 1);
    }
}
