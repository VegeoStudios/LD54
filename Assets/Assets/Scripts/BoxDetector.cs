using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    public List<Box> boxes = new List<Box>();
    public CarriageBayController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>() != null)
        {
            Box box = other.GetComponent<Box>();
            boxes.Add(box);
            if (controller.kingdom != box.kingdom)
            {
                box.wrongBin = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Box>() != null)
        {
            Box box = other.GetComponent<Box>();
            boxes.Remove(box);
            box.wrongBin = false;
        }
    }
}
