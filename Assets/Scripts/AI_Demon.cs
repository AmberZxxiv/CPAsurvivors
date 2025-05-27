using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI_Demon : MonoBehaviour
{
    public Transform objetivo;
    public float vel;
    public NavMeshAgent IA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IA.speed = vel;
        IA.SetDestination(objetivo.position);
    }


}
