using System.Collections.Generic;
using UnityEngine;

public class AIPenumpangManager : MonoBehaviour
{
    [Header("Agent Settings")]
    [Tooltip("Prefab yang akan dipanggil berdasarkan urutan")]
    public List<GameObject> spawnOrder;

    [Header("Spawn Settings")]
    public Transform spawnCenter;
    public float spawnRadius = 5f;
    public float spawnInterval = 2f;

    private Queue<GameObject> spawnQueue = new Queue<GameObject>();
    private int totalSpawned = 0;

    void Start()
    {
        foreach (var prefab in spawnOrder)
        {
            spawnQueue.Enqueue(prefab);
        }

        InvokeRepeating(nameof(SpawnNextAgent), 1f, spawnInterval);
    }

    void SpawnNextAgent()
    {
        if (spawnQueue.Count == 0)
        {
            CancelInvoke(nameof(SpawnNextAgent));
            return;
        }

        GameObject agentPrefab = spawnQueue.Dequeue();
        Vector3 randomPos = spawnCenter.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = spawnCenter.position.y;

        Instantiate(agentPrefab, randomPos, Quaternion.identity);
        totalSpawned++;
    }
}
