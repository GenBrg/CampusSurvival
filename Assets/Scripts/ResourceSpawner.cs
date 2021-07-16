using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject woodMinable;
    public GameObject scrapMetalMinable;
    public float woodProb;
    
    private List<int> unoccupiedPoints;

    public int existingPointsNum
    {
        get => spawnPoints.Count - unoccupiedPoints.Count;
    }
    
    public int maxSpawnNum
    {
        get => spawnPoints.Count / 2;
    }

    public int maxSpawnNumInOneCycle
    {
        get => spawnPoints.Count / 4;
    }

    void SpawnResource()
    {
        if (existingPointsNum >= maxSpawnNum)
        {
            return;
        }

        int numOfPointsToSpawn = Mathf.Min(maxSpawnNumInOneCycle, maxSpawnNum - existingPointsNum);

        for (int i = 0; i < numOfPointsToSpawn; ++i)
        {
            int unoccupiedIdx = Random.Range(0, unoccupiedPoints.Count);
            int spawnPosIdx = unoccupiedPoints[unoccupiedIdx];
            Transform spawnPos = spawnPoints[spawnPosIdx];
            unoccupiedPoints.RemoveAt(unoccupiedIdx);

            Minable minable = null;
            if (Random.Range(0.0f, 1.0f) < woodProb)
            {
                minable = Instantiate(woodMinable, spawnPos.position, spawnPos.rotation).GetComponent<Minable>();
            }
            else
            {
                minable = Instantiate(scrapMetalMinable, spawnPos.position, spawnPos.rotation).GetComponent<Minable>();
            }

            minable.onMined.AddListener(() =>
            {
                unoccupiedPoints.Add(spawnPosIdx);
            });
        }
    }

    private void Awake()
    {
        unoccupiedPoints = new List<int>();

        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            unoccupiedPoints.Add(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.RegisterRoutineStartsAt(0, 6, 0, SpawnResource);
    }
}
