using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float spawnDelay;
    public float delayReduction;
    private float spawnCountdown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject(spawnDelay));
    }

    IEnumerator SpawnObject(float delay)
    {
        spawnCountdown = spawnDelay;
        while(true)
        {
            yield return null;
            spawnCountdown -= Time.deltaTime;

            if(spawnCountdown <= 0 )
            {
                GameObject spawnee = ObjectPool.SharedInstance.GetPooledObject(); 
                if (spawnee != null) {
                    spawnee.transform.position = GameObject.Find("Spawner").transform.position;
                    spawnee.transform.rotation = GameObject.Find("Spawner").transform.rotation;
                    spawnee.SetActive(true);
                }

                if (delay >= .5)
                {
                    delay -= delayReduction;
                    spawnCountdown = delay;
                }

            }

        }

    }


}



