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
    public GameObject scareGhost;

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
        // susto y si tienes dinero te resta 1 y lo desactiva en la UI
        if (collision.gameObject.CompareTag("Player"))
        {
            scareGhost.SetActive(true);
            StartCoroutine(EndScare());

            if (player.money > 0)
            {
                player.money--;
                Destroy(player.earned.transform.GetChild(0).gameObject);
                //bucle pa reponer 1 moneda en algun spawner libre
                // NO TOCAR PUEDE EXPLOTAR !!!
                for (int i = 0; i < 1; i++)
                {
                    // NO TOCAR PUEDE EXPLOTAR !!!
                    GameObject spawnerToInstantiate = player.spawner.coinSpawner[Random.Range(0, player.spawner.coinSpawner.Count)];// pillo un spawner random de mi lista
                                                                                                                                    // NO TOCAR PUEDE EXPLOTAR !!!
                    while (spawnerToInstantiate.transform.childCount != 0) // cuando el hijo no sea 0, buscame otro
                    {
                        spawnerToInstantiate = player.spawner.coinSpawner[Random.Range(0, player.spawner.coinSpawner.Count)];
                    }
                    Instantiate(player.spawner.coinPref, spawnerToInstantiate.transform);// instancio una moneda en ese spawner random
                                                                                         // NO TOCAR PUEDE EXPLOTAR !!!
                    Debug.LogWarning(i);
                }
                // NO TOCAR PUEDE EXPLOTAR !!!
            }
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
        scareGhost.SetActive(false);
        print("End Scare");
    }
}
