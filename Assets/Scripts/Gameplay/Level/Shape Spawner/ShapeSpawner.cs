using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform shapesParent;
    [SerializeField]
    private ShapeData shapeData;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;


    private IDictionary<ShapeType, int> pendingShapeTypeToCountToSpawn;
    private IDictionary<ShapeType, IPool<Shape>> shapeTypeToShapePool;
    private IDictionary<ShapeType, ICollection<IShapeSpawnStrategy>> shapeTypeToShapeSpawnStrategy;

    private Coroutine spawningCoroutine;


    private void Awake()
    {
        pendingShapeTypeToCountToSpawn = new Dictionary<ShapeType, int>();
        shapeTypeToShapePool = new Dictionary<ShapeType, IPool<Shape>>();
        shapeTypeToShapeSpawnStrategy = new Dictionary<ShapeType, ICollection<IShapeSpawnStrategy>>();
    }

    void OnEnable()
    {
        levelLifecycleEventChannel.InitialisationRequested.AddListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.AddListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.AddListener(OnLevelFinished);
    }
    void OnDisable()
    {
        levelLifecycleEventChannel.InitialisationRequested.RemoveListener(OnLevelInitialisationRequested);
        levelLifecycleEventChannel.Started.RemoveListener(OnLevelStarted);
        levelLifecycleEventChannel.Finished.RemoveListener(OnLevelFinished);
    }

    private void OnLevelInitialisationRequested(LevelDatum levelDatum)
    {
        foreach (BaseShapeSpawnDatum shapeSpawnDatum in levelDatum.ShapeSpawnData)
        {
            if (!shapeTypeToShapePool.ContainsKey(shapeSpawnDatum.ShapeType))
            {
                shapeTypeToShapePool[shapeSpawnDatum.ShapeType] = new Pool<Shape>(shapeData.GetShapePrefab(shapeSpawnDatum.ShapeType), shapesParent, 32, 128);
            }

            if (!shapeTypeToShapeSpawnStrategy.ContainsKey(shapeSpawnDatum.ShapeType))
            {
                shapeTypeToShapeSpawnStrategy[shapeSpawnDatum.ShapeType] = new List<IShapeSpawnStrategy>();
            }
            shapeTypeToShapeSpawnStrategy[shapeSpawnDatum.ShapeType].Add(shapeSpawnDatum.GetShapeSpawnStrategyInstance(RequestSpawnOf));
        }
    }

    private void OnLevelStarted()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawningCoroutine != null)
        {
            return;
        }
        spawningCoroutine = StartCoroutine(HandleSpawning());
    }

    private void OnLevelFinished()
    {
        StopSpawning();
        CleanUp();
    }

    private void StopSpawning()
    {
        if (spawningCoroutine == null)
        {
            return;
        }
        StopCoroutine(spawningCoroutine);
        spawningCoroutine = null;

    }

    private void CleanUp()
    {
        pendingShapeTypeToCountToSpawn.Clear();
        foreach (IPool<Shape> shapePool in shapeTypeToShapePool.Values)
        {
            shapePool.Clear();
        }
        shapeTypeToShapePool.Clear();
    }


    private IEnumerator HandleSpawning()
    {
        while (true)
        {

            foreach (ShapeType shapeType in pendingShapeTypeToCountToSpawn.Keys)
            {
                for (int i = 0; i < pendingShapeTypeToCountToSpawn[shapeType]; i++)
                {
                    // Temporarely spawn shapes above the player.
                    Shape item = shapeTypeToShapePool[shapeType].RequestInstance();
                    Vector3 randomPosition = 5.0f * Random.insideUnitSphere;
                    randomPosition.z = 0f;
                    item.transform.localPosition = randomPosition;
                }
            }
            pendingShapeTypeToCountToSpawn.Clear();


            foreach (ICollection<IShapeSpawnStrategy> shapeSpawnStrategies in shapeTypeToShapeSpawnStrategy.Values)
            {
                foreach (IShapeSpawnStrategy shapeSpawnStrategy in shapeSpawnStrategies)
                {
                    shapeSpawnStrategy.Update(Time.fixedDeltaTime);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void RequestSpawnOf(ShapeType shapeType, int spawnCount)
    {
        if (!pendingShapeTypeToCountToSpawn.ContainsKey(shapeType))
        {
            pendingShapeTypeToCountToSpawn[shapeType] = 0;
        }
        pendingShapeTypeToCountToSpawn[shapeType] += spawnCount;
    }
}
