using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHighScore : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("highScore"))
        {
            text.text = PlayerPrefs.GetInt("highScore").ToString();
        }
        else
        {
            text.text = "0";
        }
    }
}
