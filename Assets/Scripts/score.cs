using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text HighScoreText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }


    void Update()
    {

    }
}
