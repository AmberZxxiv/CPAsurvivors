using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_PRiest : MonoBehaviour
{
    // le damos a la IA los objetivos varios
    public Transform objective;
    public int speed;
    public NavMeshAgent AI;
    private bool isFollowing;
    public Transform[] patrolPoints;
    public int targetPoint;
    public GameObject scarePriest;

    // llamo al script del player
    public Player_Movement player;

    // Start is called before the first frame update
    void Start()
    {
        // que empiece a hacer sus cosas
        isFollowing = false;
        AI.speed = speed;
        targetPoint = Random.Range(0, patrolPoints.Length);
        AI.SetDestination(patrolPoints[targetPoint].position);
        print("NEW target " + targetPoint);
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

    private void OnCollisionEnter(Collision collision)
    {
        // susto, muerte instantanea y activa el cursor
        if (collision.gameObject.CompareTag("Player"))
        {
            scarePriest.SetActive(true);
            StartCoroutine(EndScare());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.deadMenu.SetActive(true);
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
        // pilla un nuevo punto siempre que no sea el mismo de antes
        int lastCheckPoint = targetPoint;
        do
        {
            targetPoint = Random.Range(0, patrolPoints.Length);
        }
        while (targetPoint == lastCheckPoint && patrolPoints.Length > 1);

        print("NEW target " + targetPoint);
        AI.SetDestination(patrolPoints[targetPoint].position);
    }

    IEnumerator EndScare()
    {
        yield return new WaitForSeconds(1);
        scarePriest.SetActive(false);
        print("End Scare");
        Time.timeScale = 0;
        //pongo el tiempo 0 aqui porque sino nunca se desctiva el susto
    }
}
