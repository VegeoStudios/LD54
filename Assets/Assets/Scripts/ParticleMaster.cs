using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleMaster : MonoBehaviour
{
    private VisualEffect _effect;

    private void Start()
    {
        _effect = GetComponent<VisualEffect>();
    }

    public void CreateDirtParticles(Vector3 position, float strength)
    {
        _effect.SetFloat("Strength", strength);
        _effect.SetVector3("Position", position);
        _effect.Play();
    }
}
