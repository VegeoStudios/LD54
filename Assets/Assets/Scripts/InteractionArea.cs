using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    private List<Interactable> objectsInArea = new List<Interactable>();

    public bool canInteract {  get; private set; }

    public Interactable selected = null;

    private void Start()
    {
        canInteract = true;
    }

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

        if (canInteract)
        {
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
        }

        if (selected != nextSelected)
        {
            selected?.SetHovered(false);
            selected = nextSelected;
            selected?.SetHovered(true);
        }
    }

    public void SetCanInteract(bool canInteract)
    {
        this.canInteract = canInteract;
        if (!canInteract) selected?.SetHovered(false);
        selected = null;
    }
}
