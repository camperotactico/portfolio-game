using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform shapesParent;
    [SerializeField]
    private BaseShapeSpawnDatum[] shapeSpawnData;

    [SerializeField]
    private ShapeData shapeData;



    private IDictionary<ShapeType, int> pendingShapeTypeToCountToSpawn;
    private IDictionary<ShapeType, IPool<Shape>> shapeTypeToShapePool;
    private IDictionary<ShapeType, IShapeSpawnStrategy> shapeTypeToShapeSpawnStrategy;

    private Coroutine spawningCoroutine;


    private void Awake()
    {
        Debug.Log("TODO: Move shapeSpawnDatum.Shape to a scriptable object that holds a dictionary of ShapeType to ShapePrefab.");
        pendingShapeTypeToCountToSpawn = new Dictionary<ShapeType, int>();
        shapeTypeToShapePool = new Dictionary<ShapeType, IPool<Shape>>();
        shapeTypeToShapeSpawnStrategy = new Dictionary<ShapeType, IShapeSpawnStrategy>();

        foreach (BaseShapeSpawnDatum shapeSpawnDatum in shapeSpawnData)
        {
            shapeTypeToShapePool[shapeSpawnDatum.ShapeType] = new Pool<Shape>(shapeSpawnDatum.Shape, shapesParent);
            shapeTypeToShapeSpawnStrategy[shapeSpawnDatum.ShapeType] = shapeSpawnDatum.GetShapeSpawnStrategyInstance(RequestSpawnOf);
        }
    }
    private void Start()
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

            foreach(ShapeType shapeType in pendingShapeTypeToCountToSpawn.Keys)
            {
                for (int i = 0; i < pendingShapeTypeToCountToSpawn[shapeType];i++)
                {
                    Shape item = shapeTypeToShapePool[shapeType].RequestInstance();
                    Vector3 randomPosition = 3.0f * Random.insideUnitSphere;
                    randomPosition.z = 0f;
                    item.transform.localPosition = randomPosition;
                }
            }
            pendingShapeTypeToCountToSpawn.Clear();


            foreach (IShapeSpawnStrategy shapeSpawnStrategy in shapeTypeToShapeSpawnStrategy.Values)
            {
                shapeSpawnStrategy.Update(Time.fixedDeltaTime);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void RequestSpawnOf(ShapeType shapeType)
    {
        if (!pendingShapeTypeToCountToSpawn.ContainsKey(shapeType))
        {
            pendingShapeTypeToCountToSpawn[shapeType] = 0;
        }
        pendingShapeTypeToCountToSpawn[shapeType]++;
    }
}
