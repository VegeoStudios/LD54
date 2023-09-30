using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movement { get; private set; }
    public bool interact { get; private set; }
    public bool interactPressed { get; private set; }
    public bool sprint { get; private set; }

    private void LateUpdate()
    {
        interactPressed = false;
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnInteract(InputValue value)
    {
        interact = value.Get<float>() > 0.5f;
        interactPressed = interact;
    }

    private void OnSprint(InputValue value)
    {
        sprint = value.Get<float>() > 0.5f;
    }
}
