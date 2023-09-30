using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    private List<Interactable> objectsInArea = new List<Interactable>();

    public Interactable selected = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            objectsInArea.Add(other.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            if (objectsInArea.Contains(other.GetComponent<Interactable>()))
            {
                objectsInArea.Remove(other.GetComponent<Interactable>());
            }
        }
    }

    private void Update()
    {
        Interactable nextSelected = null;

        float dist = float.MaxValue;
        foreach (Interactable obj in objectsInArea)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                objectsInArea.Remove(obj);
                continue;
            }

            float objDist = Vector3.Distance(obj.transform.position, transform.position);
            if (dist > objDist)
            {
                nextSelected = obj;
                dist = objDist;
            }
        }

        if (selected != nextSelected)
        {
            selected?.SetHovered(false);
            selected = nextSelected;
            selected?.SetHovered(true);
        }
    }
}
