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

    private Rigidbody grabbedObject;

    private PlayerInputHandler input;
    private Rigidbody rb;
    [SerializeField] private SpringJoint grabbingJoint;
    [SerializeField] private InteractionArea interactionArea;

    private void Start()
    {
        AssignComponentReferences();
    }

    private void Update()
    {
        DoBoxInteraction();

        GetMovementValues();
        DoRotation();
    }

    private void FixedUpdate()
    {
        rb.velocity = targetMovement * movementParameters.maxMoveSpeed;
    }

    private void DoBoxInteraction()
    {
        if (input.interactPressed)
        {
            if (grabbedObject)
            {
                grabbedObject = null;
                grabbingJoint.connectedBody = null;
                interactionArea.SetCanInteract(true);
            }
            else
            {
                grabbedObject = interactionArea.selected?.GetComponent<Rigidbody>();
                if (grabbedObject)
                {
                    grabbingJoint.connectedBody = grabbedObject;
                    grabbingJoint.connectedAnchor = grabbedObject.transform.InverseTransformPoint(grabbedObject.GetComponent<Collider>().ClosestPoint(grabbingJoint.transform.position));
                    interactionArea.SetCanInteract(false);
                }
            }
            
        }
    }

    private void GetMovementValues()
    {
        targetMovement = RemapXYtoXZ(input.movement);
        moving = targetMovement != Vector3.zero;
    }

    private void DoRotation()
    {
        if (grabbingJoint.connectedBody)
        {
            Vector3 targetPos = grabbingJoint.connectedBody.transform.TransformPoint(grabbingJoint.connectedAnchor) - transform.position;
            targetPos = new Vector3(targetPos.x, 0, targetPos.z).normalized;
            targetAngle = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;
        }
        else
        {
            if (moving)
            {
                targetAngle = Mathf.Atan2(targetMovement.x, targetMovement.z) * Mathf.Rad2Deg;
            }
        }

        transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, movementParameters.turnSpeed * Time.deltaTime), 0);
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
