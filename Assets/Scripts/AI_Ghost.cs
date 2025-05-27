using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Ghost : MonoBehaviour
{
    public Transform objective;
    public int speed;
    public NavMeshAgent AI;
    private bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AI.speed = speed;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
        }
    }
}
