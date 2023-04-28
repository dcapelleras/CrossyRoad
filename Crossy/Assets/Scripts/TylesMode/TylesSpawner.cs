using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TylesSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> platformPrefabs; //should be a prefab of all horizontal tyles
    [SerializeField] Transform initialSpawnPos;
    [SerializeField] Vector3 nextSpawnOffset;
    Vector3 currentOffset;
    [SerializeField] int initialAmount;
    int amountSpawned = 0;

    public static TylesSpawner instance;

    private void Awake()
    {
        currentOffset = initialSpawnPos.position;
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < initialAmount; i++)
        {
            SpawnTyle();
        }
    }

    public void SpawnTyle()
    {
        int randomIndex = Random.Range(0, platformPrefabs.Count);
        if (amountSpawned == 0)
        {
            randomIndex= 1;
        }
        amountSpawned++;
        Instantiate(platformPrefabs[randomIndex], currentOffset, Quaternion.identity);
        currentOffset += nextSpawnOffset;
    }
}
