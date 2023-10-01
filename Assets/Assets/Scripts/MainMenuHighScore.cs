using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHighScore : MonoBehaviour
{
    private TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
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
