using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MagicParticleController : MonoBehaviour
{
    public static MagicParticleController instance;
    private VisualEffect _effect;

    private void Start()
    {
        instance = this;
        _effect = GetComponent<VisualEffect>();
    }

    public void CreateMagicParticles(Vector3 position, Vector3 direction)
    {
        _effect.SetVector3("Position", position);
        _effect.SetVector3("Direction", direction);
        _effect.Play();
    }
}
