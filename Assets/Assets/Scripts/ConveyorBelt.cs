using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 3.0f;

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Player")
            return;

        // Ensure that conveyor mesh is scaled towards its local Z-axis, make it long on the Z-axis

        float conveyorVelocity = speed * Time.deltaTime;

        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = conveyorVelocity * transform.forward;
    }
}
