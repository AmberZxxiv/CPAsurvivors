using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI_Demon : MonoBehaviour
{
    public Transform objective;
    public int speed;
    public NavMeshAgent AI;
    public Transform[] punishZones;
    public GameObject scareDemon;
    public AudioSource screamDemon;

    // llamo al script del player
    public Player_Movement player;

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

    private void OnCollisionEnter(Collision collision)
    {
        // te resta una vida y desactiva 1 hijo en la UI
        if (collision.gameObject.CompareTag("Player"))
        {
            screamDemon.Play();
            scareDemon.SetActive(true);
            StartCoroutine(EndScare());
            player.lifes--;
            player.health.transform.GetChild(player.lifes).gameObject.SetActive(false);
            player.gameObject.transform.position = punishZones[Random.Range(0, punishZones.Length)].position;
            // TP transform player a X coordenada random en lista
        }
    }

    IEnumerator EndScare()
    {
        yield return new WaitForSeconds(1);
        scareDemon.SetActive(false);
        print("End Scare");
    }
}
