using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    public MovementParameters movementParameters;
    public float liftStrength;
    public float breakDistance;
    public Vector3 thrustSpellForce;
    public Vector3 tossSpellForce;

    private Vector3 targetMovement;
    private float targetAngle;
    private bool moving = false;

    private float constraintWeight;

    private Rigidbody grabbedObject;
    private float grabbedObjectOriginalMass;

    private PlayerInputHandler input;
    private Rigidbody rb;
    private RandomSoundPool soundPool;
    [SerializeField] private SpringJoint grabbingJoint;
    [SerializeField] private InteractionArea interactionArea;
    [SerializeField] private Animator animator;
    [SerializeField] private RotationConstraint[] armRotationConstraints;
    [SerializeField] private PositionConstraint[] armPositionConstraints;
    [SerializeField] private Transform sweatIndicator;
    [SerializeField] private VisualEffect trail;
    [SerializeField] private RandomSoundPool spellSounds;

    private float lastFootstep;

    private void Start()
    {
        AssignComponentReferences();
    }

    private void Update()
    {
        DoSpells();
        DoBoxInteraction();
        GetMovementValues();
        DoRotation();
        AnimateArms();
        DoParticles();
        DoSound();
    }

    private void FixedUpdate()
    {
        if (grabbedObject)
        {
            if (Vector3.Distance(grabbingJoint.transform.position, grabbedObject.transform.TransformPoint(grabbingJoint.connectedAnchor)) > breakDistance)
            {
                DropBox();
            }
        }

        float multiplier = 1f;
        bool pulling = false;
        if (grabbedObject)
        {
            
            Vector3 objpos = grabbedObject.transform.position;
            objpos.y = 0;
            float dot = Vector3.Dot(targetMovement, (objpos - transform.position).normalized);
            pulling = dot < 0;
            multiplier = dot * 0.25f + 1f;
            multiplier *= Mathf.Clamp(liftStrength / grabbedObjectOriginalMass, 0.25f, 1f);
        }

        sweatIndicator.gameObject.SetActive(pulling);

        float speed = movementParameters.baseMoveSpeed;
        if (input.sprint && !grabbedObject) speed = movementParameters.sprintMoveSpeed;
        rb.AddForce(targetMovement * speed * rb.mass * multiplier);
        //rb.velocity = targetMovement * (input.sprint ? movementParameters.sprintMoveSpeed : movementParameters.baseMoveSpeed);
    }

    private void DoSound()
    {
        float currentWait = 1 / rb.velocity.magnitude;
        if (lastFootstep + currentWait < Time.time)
        {
            lastFootstep = Time.time;
            soundPool.PlaySoundFromPool();
        }
    }

    private void DoParticles()
    {
        trail.SetBool("active", rb.velocity.magnitude > 6f);
    }

    private void DoSpells()
    {
        if (!grabbedObject) return;
        if (input.thrustSpellPressed)
        {
            spellSounds.PlaySoundFromPoolPitch(1f);
            grabbedObject.AddForce(transform.rotation * thrustSpellForce * grabbedObject.mass, ForceMode.Impulse);
            MagicParticleController.instance.CreateMagicParticles(transform.position + Vector3.up + transform.rotation * thrustSpellForce.normalized * 0.5f, (transform.rotation * thrustSpellForce).normalized);
            DropBox();
        }
        else if (input.tossSpellPressed)
        {
            spellSounds.PlaySoundFromPoolPitch(0.8f);
            grabbedObject.AddForce(transform.rotation * tossSpellForce * grabbedObject.mass, ForceMode.Impulse);
            MagicParticleController.instance.CreateMagicParticles(transform.position + Vector3.up + transform.rotation * tossSpellForce.normalized * 0.5f, (transform.rotation * tossSpellForce).normalized);
            DropBox();
        }
    }

    private void AnimateArms()
    {
        constraintWeight = Mathf.Lerp(constraintWeight, grabbedObject ? 1 : 0, 5 * Time.deltaTime);

        foreach (RotationConstraint constraint in armRotationConstraints)
        {
            constraint.weight = constraintWeight;
        }

        foreach (PositionConstraint constraint in armPositionConstraints)
        {
            constraint.weight = constraintWeight;
        }
    }

    private void DoBoxInteraction()
    {
        if (input.interactPressed)
        {
            if (grabbedObject)
            {
                DropBox();
            }
            else
            {
                grabbedObject = interactionArea.selected?.GetComponent<Rigidbody>();
                if (grabbedObject)
                {
                    grabbedObject.GetComponent<Box>().grabbed = true;
                    grabbedObjectOriginalMass = grabbedObject.mass;
                    grabbedObject.mass *= Mathf.Pow(Mathf.Clamp(liftStrength / grabbedObject.mass, 0.25f, 1f), 2f);
                    grabbingJoint.connectedBody = grabbedObject;
                    grabbingJoint.connectedAnchor = Vector3.zero;
                    //grabbingJoint.connectedAnchor = grabbedObject.transform.InverseTransformPoint(grabbedObject.GetComponent<Collider>().ClosestPoint(grabbingJoint.transform.position));
                    interactionArea.SetCanInteract(false);
                }
            }
            
        }
    }

    private void DropBox()
    {
        if (!grabbedObject) return;
        grabbedObject.GetComponent<Box>().grabbed = false;
        grabbedObject.mass = grabbedObjectOriginalMass;
        grabbedObject = null;
        grabbingJoint.connectedBody = null;
        interactionArea.SetCanInteract(true);
    }

    private void GetMovementValues()
    {
        targetMovement = RemapXYtoXZ(input.movement);
        moving = targetMovement != Vector3.zero;

        animator.SetFloat("Speed", rb.velocity.magnitude);
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
        soundPool = GetComponent<RandomSoundPool>();
    }

    private Vector3 RemapXYtoXZ(Vector3 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    [System.Serializable]
    public class MovementParameters
    {
        public float baseMoveSpeed;
        public float sprintMoveSpeed;

        public float turnSpeed;
    }
}
