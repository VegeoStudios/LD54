using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Ground : MonoBehaviour
{
    private ContactPoint[] contacts = new ContactPoint[8];

    public void OnCollisionEnter(Collision collision)
    {
        int count = collision.GetContacts(contacts);
        Vector3 position = Vector3.zero;
        for (int i = 0; i < count; i++)
        {
            position += contacts[i].point;
        }
        position /= count;
        DustParticleController.instance.CreateDirtParticles(position, 1f);
    }
}
