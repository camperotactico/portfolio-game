using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private SceneReference gameplaySceneReference;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelSelectionEventChannel levelSelectionEventChannel;

    [Header("Runtime Sets")]
    [SerializeField]
    private LevelLoadRequestRuntimeSet levelLoadRequestRuntimeSet;

    void OnEnable()
    {
        levelSelectionEventChannel.LevelButtonPressed.AddListener(OnLevelButtonPressed);
    }

    void OnDisable()
    {
        levelSelectionEventChannel.LevelButtonPressed.RemoveListener(OnLevelButtonPressed);
    }

    private void OnLevelButtonPressed(LevelDatum levelDatum)
    {
        LoadLevel(levelDatum);
    }

    private void LoadLevel(LevelDatum levelDatum)
    {
        Debug.Log("TODO: Make this method asynchronous and show a Loading Screen");
        levelLoadRequestRuntimeSet.LevelDatum = levelDatum;
        SceneManager.LoadScene(gameplaySceneReference.BuildIndex);
    }
}
