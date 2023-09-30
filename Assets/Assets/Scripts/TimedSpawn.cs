using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    void SpawnObject()
    {
        GameObject spawnee = ObjectPool.SharedInstance.GetPooledObject(); 
        if (spawnee != null) {
            spawnee.transform.position = GameObject.Find("Spawner").transform.position;
            spawnee.transform.rotation = GameObject.Find("Spawner").transform.rotation;
            spawnee.SetActive(true);
        }
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }


}
