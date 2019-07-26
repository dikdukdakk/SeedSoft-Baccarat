using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostCard : MonoBehaviour
{
    public static HostCard current;

    [Header("Host Properties")]
    public TextMesh username;        //name user
    public TextMesh money;           //money user
    public Image photo;          //image user
    public float totalMoney;     //total money
    public int totalScore = 0;   //total score in one round
    public int gameStatus;

    [Header("Card Properties")]
    public GameObject[] cardHost;   //card host
    public GameObject[] bgcardHost; //background card player
    public int[] typeCard;
    public float[] scoreCard;
    public TextMesh score;  //score in round
    public SpriteRenderer bgscore;
    public GameObject X2X3;

    public int getText;
    public int getRole;
    public string checkSort; //เก็บค่าตัวเลขที่ได้เป็น String เพื่อเช็คหาไพ่เรียง


    void LateUpdate()
    {
        GetScore();
        ShowUIText(getText);
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

    public void UnActiveAniamtion() //Active Animation
    {
        for (int i = 0; i < 3; i++)
        {
            cardHost[i].GetComponent<Animator>().enabled = false;
            bgcardHost[i].GetComponent<Animator>().enabled = false;
        }

    }

    public void ShowUIText(int changeText)
    {
        switch (changeText)
        {
            case 1: score.text = "" + totalScore + " แต้ม"; break;
            case 4: score.text = "ไพ่เรียง"; break;
            case 6: score.text = "ไพ่เซียน"; break;
            case 5: score.text = "ไพ่ตอง"; break;

            case 8: score.text = "ป๊อก " + totalScore; break;
        }
    }

}
