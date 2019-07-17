using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostCard : MonoBehaviour
{
    public static HostCard current;

    [Header("Host Properties")]
    public Text username;        //name user
    public Text money;           //money user
    public Image photo;          //image user
    public float totalMoney;     //total money
    public int totalScore = 0;   //total score in one round
    public Text score;           //score in round


    [Header("Card Properties")]
    public GameObject X2X3;
    public GameObject ch1;       //card host
    public GameObject ch2,ch3; 
    public GameObject bgch1, bgch2, bgch3; //background card player
    public int typeCard1, typeCard2, typeCard3;
    

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

    public void ActiveAniamtion(int cntCard) //Active Animation
    {
        ch1.GetComponent<Animator>().enabled = true;
        bgch1.GetComponent<Animator>().enabled = true;
        ch2.GetComponent<Animator>().enabled = true;
        bgch2.GetComponent<Animator>().enabled = true;

        if (cntCard == 3)
        {
            ch3.GetComponent<Animator>().enabled = true;
            bgch3.GetComponent<Animator>().enabled = true;
        }

    }

    public void ShowUIText()
    {
        score.text = "" + totalScore + " แต้ม";
    }
}
