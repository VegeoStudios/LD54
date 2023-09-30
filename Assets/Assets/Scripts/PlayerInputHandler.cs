using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movement { get; private set; }
    public bool interact { get; private set; }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnInteract(InputValue value)
    {
        interact = value.Get<float>() > 0.5f;
    }
}
