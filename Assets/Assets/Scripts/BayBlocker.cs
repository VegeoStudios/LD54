using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayBlocker : MonoBehaviour
{
    public bool playerInside;

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void TurnOnBlocker()
    {
        boxCollider.isTrigger = false;
    }

    public void TurnOffBlocker()
    {
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInside = false;
        }
    }
}
