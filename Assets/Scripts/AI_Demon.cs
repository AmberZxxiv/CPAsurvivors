using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI_Demon : MonoBehaviour
{
    public Transform objective;
    public int speed;
    public NavMeshAgent AI;

    // Start is called before the first frame update
    void Start()
    {
        AI.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        AI.SetDestination(objective.position);
    }
}
