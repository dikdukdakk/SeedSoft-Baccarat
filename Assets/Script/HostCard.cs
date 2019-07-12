using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostCard : MonoBehaviour
{
    public static HostCard current;

    [Header("Host Properties")]
    public Text username;
    public Text money;
    public Text score;
    public Image photo;
    public float totalScore = 0;
    public float totalMoney;

    [Header("Card Properties")]
    public GameObject ch1; //card host
    public GameObject ch2,ch3; 
    public GameObject bgch1, bgch2, bgch3; //background card player

    void LateUpdate()
    {
        GetScore();

        ShowUIText();
    }

    public void GetScore()
    {
        if (totalScore >= 10)
            totalScore -= 10;
        else if (totalScore >= 20)
            totalScore -= 20;
    }

    public void ShowUIText()
    {
        score.text = "" + totalScore + " แต้ม";
    }
}
