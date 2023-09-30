using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : MonoBehaviour
{
    
    public float smoothing = 0.01f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(arrive(target));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator arrive(Transform target)
    {
        while(Vector3.Distance(transform.position, target.position) > 15)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
        }
        yield return new WaitForSeconds(30);

        
    }
}
