using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public Text ScoreTxt;
    public Text CoinsTxt;
    public Text Record_score;
    public GameObject Result;
    public PlayerMovement pm;
    public float maxSpeed = 10;
    public float Score;
    public float HighScore;
    public float Coins = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Score = 0;
    }

    void Update()
    {
        if(RoadGenerator.instance.speed != 0)
            Score += Time.deltaTime * 10;

        ScoreTxt.text = ((int)Score).ToString();
       // HighScore

    }

    public void AddCoins(int number)
    {
        Coins += number;
        CoinsTxt.text = Coins.ToString();
    }

    public void ShowResult()
    {
        Result.SetActive(true);
        PlayerPrefs.SetInt("HighScore", (int)Score);
        Record_score.text = "Score: " + ((int)Score).ToString();

    }



    public void StartGame()
    {
        RoadGenerator.instance.speed = maxSpeed;
        Result.SetActive(false);
        pm.canPlay = true;
    }
}
