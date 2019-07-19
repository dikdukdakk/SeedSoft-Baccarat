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
    public int statusHost;       //host get X2 X3 X5 (2 3 5)

    [Header("Card Properties")]
    public GameObject[] cardHost;   //card host
    public GameObject[] bgcardHost; //background card player
    public int[] typeCard;
    public float[] scoreCard;
    public string checkSort; //เก็บค่าตัวเลขที่ได้เป็น String เพื่อเช็คหาไพ่เรียง
    public GameObject X2X3;


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
        for (int i = 0; i < 2; i++)
        {
            cardHost[i].GetComponent<Animator>().enabled = true;
            bgcardHost[i].GetComponent<Animator>().enabled = true;
        }

        if (cntCard == 3)
        {
            cardHost[2].GetComponent<Animator>().enabled = true;
            bgcardHost[2].GetComponent<Animator>().enabled = true;
        }

    }

    public void ShowUIText()
    {
        score.text = "" + totalScore + " แต้ม";
    }
}
