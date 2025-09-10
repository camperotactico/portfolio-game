using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform shapesParent;
    [SerializeField]
    private ShapeData shapeData;

    [Header("Runtime Sets")]
    [SerializeField]
    private SpawnedShapesRuntimeSet spawnedShapesRuntimeSet;

    [Header("Receiving Event Channels")]
    [SerializeField]
    private LevelLifecycleEventChannel levelLifecycleEventChannel;


    private IDictionary<ShapeDatum, int> pendingShapeDatumToCountToSpawn;
    private IDictionary<ShapeDatum, IPool<Shape>> shapeDatumToShapePool;
    private IDictionary<ShapeDatum, ICollection<IShapeSpawnStrategy>> shapeDatumToShapeSpawnStrategy;

    private Coroutine spawningCoroutine;


    private void Awake()
    {
        pendingShapeDatumToCountToSpawn = new Dictionary<ShapeDatum, int>();
        shapeDatumToShapePool = new Dictionary<ShapeDatum, IPool<Shape>>();
        shapeDatumToShapeSpawnStrategy = new Dictionary<ShapeDatum, ICollection<IShapeSpawnStrategy>>();
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
            if (!shapeDatumToShapePool.ContainsKey(shapeSpawnDatum.ShapeDatum))
            {
                shapeDatumToShapePool[shapeSpawnDatum.ShapeDatum] = new Pool<Shape>(shapeData.GetShapePrefab(shapeSpawnDatum.ShapeDatum), shapesParent, 32, 128);
            }

            if (!shapeDatumToShapeSpawnStrategy.ContainsKey(shapeSpawnDatum.ShapeDatum))
            {
                shapeDatumToShapeSpawnStrategy[shapeSpawnDatum.ShapeDatum] = new List<IShapeSpawnStrategy>();
            }
            shapeDatumToShapeSpawnStrategy[shapeSpawnDatum.ShapeDatum].Add(shapeSpawnDatum.GetShapeSpawnStrategyInstance(RequestSpawnOf));
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
        ICollection<Shape> allActiveShapes = spawnedShapesRuntimeSet.GetActiveSpawnedShapes();
        foreach (Shape activeShape in allActiveShapes)
        {
            activeShape.ReleaseToPool();
        }


        pendingShapeDatumToCountToSpawn.Clear();
        foreach (IPool<Shape> shapePool in shapeDatumToShapePool.Values)
        {
            shapePool.Clear();
        }
        shapeDatumToShapePool.Clear();
    }


    private IEnumerator HandleSpawning()
    {
        while (true)
        {

            foreach (ShapeDatum shapeDatum in pendingShapeDatumToCountToSpawn.Keys)
            {
                for (int i = 0; i < pendingShapeDatumToCountToSpawn[shapeDatum]; i++)
                {
                    // Temporarely spawn shapes above the player.
                    Shape item = shapeDatumToShapePool[shapeDatum].RequestInstance();
                    Vector3 randomPosition = 2.5f * Random.insideUnitSphere;
                    randomPosition.z = 0f;
                    item.transform.localPosition = randomPosition;
                }
            }
            pendingShapeDatumToCountToSpawn.Clear();


            foreach (ICollection<IShapeSpawnStrategy> shapeSpawnStrategies in shapeDatumToShapeSpawnStrategy.Values)
            {
                foreach (IShapeSpawnStrategy shapeSpawnStrategy in shapeSpawnStrategies)
                {
                    shapeSpawnStrategy.Update(Time.fixedDeltaTime);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void RequestSpawnOf(ShapeDatum shapeDatum, int spawnCount)
    {
        if (!pendingShapeDatumToCountToSpawn.ContainsKey(shapeDatum))
        {
            pendingShapeDatumToCountToSpawn[shapeDatum] = 0;
        }
        pendingShapeDatumToCountToSpawn[shapeDatum] += spawnCount;
    }
}
