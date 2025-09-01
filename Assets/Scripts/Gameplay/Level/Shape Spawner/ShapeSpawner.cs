using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform shapesParent;
    [SerializeField]
    private ShapeData shapeData;



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

    public void OnLevelInitialisationRequested(LevelDatum levelDatum)
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

    public void OnLevelStarted()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawningCoroutine != null)
        {
            return;
        }
        spawningCoroutine = StartCoroutine(HandleSpawning());
    }

    public void StopSpawning()
    {
        if (spawningCoroutine == null)
        {
            return;
        }
        StopCoroutine(spawningCoroutine);
        spawningCoroutine = null;

        pendingShapeTypeToCountToSpawn.Clear();
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
