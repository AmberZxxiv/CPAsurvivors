using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Esto pa coger la lista y prefabs de las Coins
    public List<GameObject> coinSpawner = new ();
    public GameObject coinPref;
    public int coinsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //bucle pa poner 5 monedas en los 10 spawners
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject spawnerToInstantiate = coinSpawner[Random.Range(0, coinSpawner.Count)];// pillo un spawner random de mi lista
            while (spawnerToInstantiate.transform.childCount != 0) // cuando el hijo no sea 0, buscame otro
            {
                spawnerToInstantiate = coinSpawner[Random.Range(0, coinSpawner.Count)];
            }
            Instantiate(coinPref, spawnerToInstantiate.transform);// instancio una moneda en ese spawner random
            Debug.LogWarning(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
