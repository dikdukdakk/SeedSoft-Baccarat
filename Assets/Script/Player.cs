using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player current;

    [Header ("Player Properties")]
    public Text username;        //name user
    public Text money;           //money user
    public Image photo;          //image user
    public float totalMoney;     //total money
    public int totalScore = 0;   //total score in one round
    public Text score;           //score in round
    public int getStar = 1;     //player get X2 X3 X5 (2 3 5)

    [Header("Card Properties")]
    public GameObject[] cardPlayer;
    public GameObject[] bgcardPlayer;  //bg card
    public int[] typeCard;
    public float[] scoreCard;
    public Image bgscore;
    public string checkSort; //เก็บค่าตัวเลขที่ได้เป็น String เพื่อเช็คหาไพ่เรียง
    public GameObject X2X3;

    public bool requestCard;   //player4 request card when score greater than 3



    void Start()
    {
        current = this;
    }

    void LateUpdate()
    {
        GetScore();   //calculate score

        ShowUIText(getStar); //show all text user
    }

    public void ActiveAniamtion(int cntCard) //Active Animation
    {
        for(int i=0;i<2;i++)
        {
            cardPlayer[i].GetComponent<Animator>().enabled = true;
            bgcardPlayer[i].GetComponent<Animator>().enabled = true;
        }

        if (cntCard == 3)
        {
            cardPlayer[2].GetComponent<Animator>().enabled = true;
            bgcardPlayer[2].GetComponent<Animator>().enabled = true;
        }
              
    }

    public void GetScore() // +- score
    {
        if (totalScore >= 10) //score greater than 9 => -10
            totalScore -= 10;
        else if(totalScore >= 20) //score greater than 18 => -20
            totalScore -= 20;
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
