using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(SceneReference sceneReference)
    {
        Debug.Log("TODO: Make this method asynchronous and show a Loading Screen");
        SceneManager.LoadScene(sceneReference.BuildIndex);
    }

}
