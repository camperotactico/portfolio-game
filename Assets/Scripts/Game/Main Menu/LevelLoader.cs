using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private SceneReference gameplaySceneReference;

    [Header("Runtime Sets")]
    [SerializeField]
    private LevelLoadRequestRuntimeSet levelLoadRequestRuntimeSet;


    public void LoadLevel(LevelDatum levelDatum)
    {
        Debug.Log("TODO: Make this method asynchronous and show a Loading Screen");
        levelLoadRequestRuntimeSet.LevelDatum = levelDatum;
        SceneManager.LoadScene(gameplaySceneReference.BuildIndex);
    }
}
