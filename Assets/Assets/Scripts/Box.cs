using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private KingdomAssets assets;

    public int kingdom;

    private MeshRenderer colorIndicator;

    public bool grabbed;
    public bool wrongBin;

    protected override void Start()
    {
        base.Start();
        colorIndicator = transform.GetChild(0).GetComponent<MeshRenderer>();
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

    public void SetBoxType(int type)
    {
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
