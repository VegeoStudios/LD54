using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected Outline outline;
    public bool hovered;

    protected virtual void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void SetHovered(bool val)
    {
        hovered = val;
    }
}
