using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float spawnDelay;
    public float delayReduction;
    private float spawnCountdown;
    [HideInInspector] public int activeCount;

    // Start is called before the first frame update
    void Start()
    {
        Spawnee();
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
                if (delay <= .5f)
                {
                    spawnCountdown = .5f;
                }
                else
                {
                    delay -= delayReduction;
                    spawnCountdown = delay;
                }

                Spawnee(); 

            }
        }
    }

    void Spawnee()
    {
        GameObject spawnee = ObjectPool.SharedInstance.GetPooledObject(); 
        if (spawnee != null) {
            spawnee.transform.position = new Vector3(Random.Range(-9, 10), 20, Random.Range(-9, 10));
            spawnee.SetActive(true);
            spawnee.GetComponent<Box>().Init();
            activeCount++;
        }
    }


}



