using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player current;

    [Header ("Player Properties")]
    public Text username;
    public Text money;
    public Text score;
    public Image photo;
    public int totalScore;
    public int totalMoney;

    [Header("Card Properties")]
    public GameObject cardPlayer1;
    public GameObject cardPlayer2; //card host
    public GameObject cardPlayer3;
    public GameObject bgc1, bgc2, bgc3;
    public bool requestCard;


    void Start()
    {
        current = this;
    }

    void LateUpdate()
    {
        GetScore();

        ShowUIText();
    }

    public void GetScore()
    {
        if (totalScore >= 10)
            totalScore -= 10;
        else if(totalScore >= 20)
            totalScore -= 20;

    }

    public void ShowUIText()
    {
        score.text = "" + totalScore + " แต้ม";
    }

}
