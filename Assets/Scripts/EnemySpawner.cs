using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnMinPoint;
    public Transform spawnMaxPoint;
    public Vector2 spawnRadiusRange;

    public GameObject zombie;

    public Vector2 spawnIntervalRange;
    public int maxSpawnNum = 8;

    private Transform playerTransform;
    private LayerMask groundLayer;
    private LayerMask groundStructureLayer;

    private float lastSpawnTime;

    private const float raycastHeight = 100.0f;
    private const int maxSearchCountInOneFrame = 10;

    private int zombieNum = 0;
    private float nextSpawnInterval = 0.0f;
    private static EnemySpawner _instance;

    public int ZombieNum
    {
        get => zombieNum;
        set => zombieNum = value;
    }

    public static EnemySpawner Instance
    {
        get => _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = CharacterMovement.Instance.transform;
        groundLayer = LayerMask.NameToLayer("Ground");
        groundStructureLayer = LayerMask.GetMask("Ground", "Structure");
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's available to spawn
        if (zombieNum >= maxSpawnNum || Time.time - lastSpawnTime < nextSpawnInterval)
        {
            return;
        }

        // Try maxSearchCountInOneFrame times to spawn a zombie
        for (int i = 0; i < maxSearchCountInOneFrame; ++i)
        {
            float spawnX = Random.Range(spawnMinPoint.position.x, spawnMaxPoint.position.x);
            float spawnZ = Random.Range(spawnMinPoint.position.z, spawnMaxPoint.position.z);
            if (!Physics.Raycast(new Ray(new Vector3(spawnX, raycastHeight, spawnZ), Vector3.down), out RaycastHit hit, 500.0f, ~groundLayer))
            {
                continue;
            }

            if (hit.point.y < spawnMinPoint.position.y || hit.point.y > spawnMaxPoint.position.y)
            {
                continue;
            }

            float distToPlayer = Vector3.Distance(playerTransform.position, hit.point);

            if (distToPlayer < spawnRadiusRange.x || distToPlayer > spawnRadiusRange.y)
            {
                continue;
            }

            // Cannot spawn in player's sight 
            if (!Physics.Raycast(new Ray(hit.point + Vector3.up * 1.0f, playerTransform.position - hit.point + Vector3.up * 1.0f), distToPlayer, ~groundStructureLayer))
            {
                continue;
            }

            if (Time.time - lastSpawnTime < nextSpawnInterval)
            {
                continue;
            }

            // Spawn
            ++zombieNum;
            lastSpawnTime = Time.time;
            nextSpawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            Instantiate(zombie, hit.point, Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));

            break;
        }
    }
}
