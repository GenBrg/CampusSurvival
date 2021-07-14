using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<Vector3> spawnPoints;
    

    void Spawn()
    {
        foreach (Vector3 spawnPoint in spawnPoints)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.RegisterRoutineStartsAt(0, 6, 0, () =>
        {


        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
