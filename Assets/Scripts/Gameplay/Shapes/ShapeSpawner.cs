using System.Collections;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform shapesParent;
    [SerializeField]
    private Shape itemToSpawn;

    private IPool<Shape> shapePool;

    private void Awake()
    {
        shapePool = new Pool<Shape>(itemToSpawn, shapesParent);
    }
    private void Start()
    {
        StartCoroutine(SpawnStuff());
        
    }


    private IEnumerator SpawnStuff()
    {
        while (true)
        {
            Shape item = shapePool.RequestInstance();
            Vector3 randomPosition = 3.0f * Random.insideUnitSphere;
            randomPosition.z = 0f;
            item.transform.localPosition = randomPosition;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
