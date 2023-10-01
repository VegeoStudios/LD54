using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class CarriageBayController : MonoBehaviour
{
    [SerializeField] private KingdomAssets kingdomAssets;

    public PositionConstraint constraint;
    public BayBlocker bayBlocker;
    public BoxDetector boxDetector;
    public Transform boxesParent;
    public MeshRenderer kingdomDisplay;
    public TextMeshPro timerText;

    public int kingdom;

    public float animationTime;

    public float goalTime = 60.0f;

    public float timerValue = 0f;
    public bool timerCounting = false;

    public bool tryingToLeave = false;

    private void Start()
    {
        ResetCarriage();
    }

    private void Update()
    {
        if (timerCounting)
        {
            timerValue -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timerValue).ToString();

            if (timerValue < 0f && !tryingToLeave)
            {
                tryingToLeave = true;
                StartCoroutine(Leave());
            }
        }
    }

    private IEnumerator Arrive()
    {
        SetKingdom(Random.Range(0, kingdomAssets.timers.Length));

        float startTime = Time.time;
        while (Time.time - startTime < animationTime)
        {
            float t = (Time.time - startTime) / animationTime;
            constraint.weight = Mathf.Clamp01(Mathf.SmoothStep(1f, 0f, t));
            yield return null;
        }
        constraint.weight = 0f;

        bayBlocker.TurnOffBlocker();

        timerValue = goalTime;
        timerCounting = true;
    }

    private IEnumerator Leave()
    {
        yield return new WaitUntil(CanLeave);

        tryingToLeave = false;
        timerCounting = false;

        foreach (Box box in boxDetector.boxes)
        {
            box.rb.isKinematic = true;
            box.transform.parent = boxesParent;
        }

        bayBlocker.TurnOnBlocker();

        float startTime = Time.time;
        while (Time.time - startTime < animationTime)
        {
            float t = (Time.time - startTime) / animationTime;
            constraint.weight = Mathf.Clamp01(Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        constraint.weight = 1f;

        ResetCarriage();
    }

    private void ResetCarriage()
    {
        while (boxesParent.childCount > 0)
        {
            Transform obj = boxesParent.GetChild(0);
            Destroy(obj);
        }

        boxDetector.boxes = new List<Box>();

        timerText.text = string.Empty;

        StartCoroutine(Arrive());
    }

    private bool CanLeave()
    {
        foreach (Box box in boxDetector.boxes)
        {
            if (box.kingdom != kingdom) return false;
        }
        return !bayBlocker.playerInside;
    }

    private void SetKingdom(int kingdom)
    {
        this.kingdom = kingdom;
        kingdomDisplay.material.SetColor("_BaseColor", kingdomAssets.colors[kingdom]);
        kingdomDisplay.material.SetColor("_EmissionColor", kingdomAssets.colors[kingdom]);
        kingdomDisplay.material.SetTexture("_BaseMap", kingdomAssets.textures[kingdom]);
        kingdomDisplay.material.SetTexture("_EmissionMap", kingdomAssets.textures[kingdom]);
        goalTime = kingdomAssets.timers[kingdom] * 0.2f;
    }
}
