using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementParameters movementParameters;
    public float liftStrength;

    private Vector3 targetMovement;
    private float targetAngle;
    private bool moving = false;

    private PlayerInputHandler input;
    private Rigidbody rb;

    public enum FacingMode
    {
        MOVEMENT, TARGET
    }
    private FacingMode facingMode;
    private Vector3 facingTarget;


    private void Start()
    {
        AssignComponentReferences();
    }

    private void Update()
    {
        targetMovement = RemapXYtoXZ(input.movement);
        moving = targetMovement != Vector3.zero;
        
        switch (facingMode)
        {
            case FacingMode.MOVEMENT:
                if (moving)
                {
                    targetAngle = Mathf.Atan2(targetMovement.x, targetMovement.z) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, movementParameters.turnSpeed * Time.deltaTime), 0);
                }
                break;
            case FacingMode.TARGET:

                break;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = targetMovement * movementParameters.maxMoveSpeed;
    }

    private void AssignComponentReferences()
    {
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    private Vector3 RemapXYtoXZ(Vector3 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    [System.Serializable]
    public class MovementParameters
    {
        public float maxMoveSpeed;
        public float moveAcceleration;

        public float turnSpeed;
    }
}
