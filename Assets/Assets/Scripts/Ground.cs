using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Ground : MonoBehaviour
{
    public ParticleMaster particleMaster;

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
        particleMaster.CreateDirtParticles(position, 0.5f);
    }
}
