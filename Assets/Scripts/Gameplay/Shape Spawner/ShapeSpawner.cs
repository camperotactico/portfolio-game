using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform shapesParent;
    [SerializeField]
    private GameObject itemToSpawn;

    private void Start()
    {
        StartCoroutine(SpawnStuff());
        
    }


    private IEnumerator SpawnStuff()
    {
        while (true)
        {
            GameObject item = Instantiate(itemToSpawn);
            item.transform.SetParent(shapesParent);
            Vector3 randomPosition = 3.0f * Random.insideUnitSphere;
            randomPosition.z = 0f;
            item.transform.localPosition = randomPosition;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
