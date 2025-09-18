using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private SceneReference gameplaySceneReference;

    [Header("Receiving Event Channels")]
    public LevelDatumEventChannel LevelButtonPressed;

    [Header("Runtime Sets")]
    public LevelLoadRequestRuntimeSet LevelLoadRequestRuntimeSet;

    void OnEnable()
    {
        LevelButtonPressed.AddListener(OnLevelButtonPressed);
    }

    void OnDisable()
    {
        LevelButtonPressed.RemoveListener(OnLevelButtonPressed);
    }

    private void OnLevelButtonPressed(LevelDatum levelDatum)
    {
        LoadLevel(levelDatum);
    }

    private void LoadLevel(LevelDatum levelDatum)
    {
        Debug.Log("TODO: Make this method asynchronous and show a Loading Screen");
        LevelLoadRequestRuntimeSet.LevelDatum = levelDatum;
        SceneManager.LoadScene(gameplaySceneReference.BuildIndex);
    }
}
