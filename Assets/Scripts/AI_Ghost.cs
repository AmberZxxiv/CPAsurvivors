using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Ghost : MonoBehaviour
{
    // le damos a la IA los objetivos varios
    public Transform objective;
    public int speed;
    public NavMeshAgent AI;
    private bool isFollowing;
    public Transform[] patrolPoints;
    public int targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        // que empiece a hacer sus cosas
        isFollowing = false;
        AI.speed = speed;
        targetPoint = 0;
        AI.SetDestination(patrolPoints[targetPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        // si no me sigue, que haga su ruta
        if (!isFollowing)
        {
            AI.SetDestination(patrolPoints[targetPoint].position);
            
            // si está suficientemente cerca del checkpoint, que cambie
            if (!AI.pathPending && AI.remainingDistance < 0.5f)
            {
                print("CHECK point " + targetPoint);
                NextTarget();
            }
        }

      // si me sigue, que vaya a por mi
        if (isFollowing)
        {
           AI.SetDestination(objective.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
            print("FOLLOW");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
            print("NOT follow");
        }
    }

    void NextTarget()
    {
        // que sume 1 a la lista y vaya pa alla
        targetPoint = (targetPoint + 1) % patrolPoints.Length;
        print("NEW target " + targetPoint);
        AI.SetDestination(patrolPoints[targetPoint].position);
    }
}
