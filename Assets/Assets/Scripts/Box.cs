using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private KingdomAssets assets;

    public int kingdom;

    private MeshRenderer colorIndicator;
    public Rigidbody rb;
    private RandomSoundPool soundPool;

    public bool grabbed;
    public bool wrongBin;

    protected override void Awake()
    {
        base.Awake();
        colorIndicator = transform.GetChild(0).GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        soundPool = GetComponent<RandomSoundPool>();
        //StartCoroutine(PartyRoutine());
    }

    private void Update()
    {
        if (grabbed)
        {
            outline.enabled = true;
            outline.OutlineColor = Color.blue;
        }
        else if (hovered)
        {
            outline.enabled = true;
            outline.OutlineColor = Color.white;
        }
        else if (wrongBin)
        {
            outline.enabled = true;
            outline.OutlineColor = Color.red;
        }
        else
        {
            outline.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        soundPool.PlaySoundFromPoolVolume(Mathf.Min(collision.relativeVelocity.magnitude * 0.05f, 0.4f));
    }

    public void Init()
    {
        SetBoxType(Random.Range(0, GameManager.instance.maxKingdom + 1));
        rb.isKinematic = false;
    }

    public void SetBoxType(int type)
    {
        kingdom = type;
        colorIndicator.material.SetColor("_BaseColor", assets.colors[type]);
        colorIndicator.material.SetColor("_EmissionColor", assets.colors[type]);
        colorIndicator.material.SetTexture("_BaseMap", assets.textures[type]);
        colorIndicator.material.SetTexture("_EmissionMap", assets.textures[type]);
    }

    // A debugging coroutine, don't worry about this
    private IEnumerator PartyRoutine()
    {
        while (true)
        {
            kingdom = (kingdom + 1) % assets.textures.Length;
            SetBoxType(kingdom);
            yield return new WaitForSeconds(1f);
        }
    }
}
