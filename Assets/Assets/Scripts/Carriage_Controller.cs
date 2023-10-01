using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Carriage : MonoBehaviour
{
    public PositionConstraint constraint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(arrive(constraint));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator arrive(PositionConstraint positionConstraint)
    {
        positionConstraint.weight = 1;
        yield return new WaitForSeconds(30);
        positionConstraint.weight = 0;
        
    }
}
