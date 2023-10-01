using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] private Image statsBarFill;
    [SerializeField] private Text statsBarText;
    public float maximum;
    public string barName;
    [HideInInspector] public float bar;

    private void Start()
    {
        UpdateStatsBar(bar, maximum, barName);
    }

    public void UpdateStatsBar(float current, float maximum, string statsName)
    {
        statsBarFill.fillAmount = current / maximum;

        statsBarText.text = statsName + ": " + current + "/" + maximum;
    }

    void Update()
    {
        bar = spawner.GetComponent<Spawn>().activeCount;
        UpdateStatsBar(bar, maximum, barName);
        if (bar > maximum)
        {
            FindObjectOfType<GameManager>().RestartGame();
        }
    }


}
