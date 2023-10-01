using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    public List<Box> boxes = new List<Box>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>() != null)
        {
            boxes.Add(other.GetComponent<Box>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Box>() != null)
        {
            boxes.Remove(other.GetComponent<Box>());
        }
    }
}
