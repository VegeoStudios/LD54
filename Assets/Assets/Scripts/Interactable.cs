using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Outline outline;
    public bool hovered;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void SetHovered(bool val)
    {
        hovered = val;
        outline.enabled = val;
    }
}
