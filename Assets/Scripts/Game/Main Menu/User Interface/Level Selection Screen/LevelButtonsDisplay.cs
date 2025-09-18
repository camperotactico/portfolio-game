using UnityEngine;
using UnityEngine.UI;

public class LevelButtonsDisplay : MonoBehaviour
{
    [Header("Receiving Event Channels")]
    public VoidEventChannel LevelDataRequested;
    public VoidEventChannel LevelDataReady;

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
        LevelDataRequested.AddListener(OnLevelDataRequested);
        LevelDataReady.AddListener(OnLevelDataReady);
        previousPageButton.onClick.AddListener(OnPreviousPageButtonPressed);
        nextPageButton.onClick.AddListener(OnNextPageButtonPressed);
    }

    void OnDisable()
    {
        LevelDataRequested.RemoveListener(OnLevelDataRequested);
        LevelDataReady.RemoveListener(OnLevelDataReady);
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

        totalButtonPages = availableLevelDataRuntimeSet.LevelCount / levelButtons.Length;
        if (availableLevelDataRuntimeSet.LevelCount % levelButtons.Length > 0)
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
        int startLevelID = (currentButtonPageIndex * levelButtons.Length) + 1;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelID = startLevelID + i;
            if (availableLevelDataRuntimeSet.TryGetLevelDatum(levelID, out LevelDatum levelDatum))
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].SetLevelDatum(levelDatum);
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
