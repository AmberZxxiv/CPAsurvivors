using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> coinSpawner = new ();
    public GameObject coinPref;
    public int coinsToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i < coinsToSpawn; i++)
        {
            GameObject spawnerToInstantiate = coinSpawner[Random.Range(0,coinSpawner.Count)];//Pillo un spawner random de mi lista
            while (spawnerToInstantiate.transform.childCount!=0)
            {
                spawnerToInstantiate = coinSpawner[Random.Range(0, coinSpawner.Count)];
            }
            Instantiate(coinPref, spawnerToInstantiate.transform);//Instancio una moneda en ese spawner random
            Debug.LogWarning(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
