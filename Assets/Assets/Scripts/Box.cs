using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private BoxTypeAssets assets;

    public int boxType;

    private MeshRenderer colorIndicator;

    protected override void Start()
    {
        base.Start();
        colorIndicator = transform.GetChild(0).GetComponent<MeshRenderer>();
        //StartCoroutine(PartyRoutine());
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
            boxType = (boxType + 1) % assets.textures.Length;
            SetBoxType(boxType);
            yield return new WaitForSeconds(1f);
        }
    }
}
