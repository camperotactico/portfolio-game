using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{

    [Header("Level Parameters")]
    public LevelDatum LevelDatum;

    [Header("Game Events")]
    [SerializeField]
    private UnityEvent<LevelDatum> InitialisationRequested;
    [SerializeField]
    private UnityEvent Started;
    [SerializeField]
    private UnityEvent Finished;


    public void Start()
    {
        Debug.Log("TODO: Move this from here");
        Application.targetFrameRate = 0;

        InitialisationRequested?.Invoke(LevelDatum);
        Started?.Invoke();
    }
}