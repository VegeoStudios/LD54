using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DustParticleController : MonoBehaviour
{
    public static DustParticleController instance;
    private VisualEffect _effect;

    private void Start()
    {
        instance = this;
        _effect = GetComponent<VisualEffect>();
    }

    public void CreateDirtParticles(Vector3 position, float strength)
    {
        _effect.SetFloat("Strength", strength);
        _effect.SetVector3("Position", position);
        _effect.Play();
    }
}
