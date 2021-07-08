using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float detectionFOV = 210.0f;
    public float detectionRadius = 10.0f;
    public float loseRadius = 15.0f;
    public float detectAnywayRadius = 3.0f;
    public float fireRadius = 1.5f;
    public float timeToLose = 3.0f;
    public float rotationSpeed = 60.0f;
    
    private Transform playerTransform;
    private NavMeshAgent agent;
    private bool isChasing;
    private float losingTimer;
    private LayerMask eyesightLayer;
    private IEnemyBehavior enemyBehavior;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = fireRadius;
        eyesightLayer = LayerMask.GetMask("Ground", "Structure");
        enemyBehavior = GetComponent<IEnemyBehavior>();
    }

    bool PlayerInSight()
    {
        float chaseDist = isChasing ? loseRadius : detectionRadius;

        Vector3 playerDir = playerTransform.position - transform.position;

        if (playerDir.magnitude > chaseDist)
        {
            return false;
        }

        if (playerDir.magnitude > detectAnywayRadius && Vector3.Angle(transform.forward, playerDir) > (detectionFOV / 2.0f))
        {
            return false;
        }

        // TODO Check wall
        //if (Physics.Raycast(new Ray(transform.position, playerDir), playerDir.magnitude, ~eyesightLayer))
        //{
        //    return false;
        //}

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity == Vector3.zero)
        {
            enemyBehavior.OnNotMoving();
        } 
        else
        {
            enemyBehavior.OnMoving();
        }

        if (isChasing)
        {
            if (PlayerInSight())
            {
                losingTimer = 0.0f;
                Vector3 playerDir = playerTransform.position - transform.position;
                if (playerDir.magnitude < fireRadius)
                {
                    if (Vector3.Angle(playerDir, transform.forward) > 10.0f)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerDir, Vector3.up), Time.deltaTime * rotationSpeed);
                    }
                    else
                    {
                        enemyBehavior.OnStopChase();
                        enemyBehavior.Fire();
                    }
                }
                else
                {
                    enemyBehavior.OnChase();
                    agent.destination = playerTransform.position;
                }
            }
            else
            {
                losingTimer += Time.deltaTime;
                if (losingTimer > timeToLose)
                {
                    isChasing = false;
                    enemyBehavior.OnStopChase();
                }
            }
        }
        else
        {
            if (PlayerInSight())
            {
                isChasing = true;
                losingTimer = 0.0f;
                enemyBehavior.OnChase();
            }
        }
    }
}
