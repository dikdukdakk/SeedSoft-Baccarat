using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player current;

    [Header ("Player Properties")]
    public TextMesh username;        //name user
    public TextMesh money;           //money user
    public Sprite photo;          //image user
    public float totalMoney;     //total money
    public int totalScore = 0;   //total score in one round
    public int playerBet;        //1,2,3,4  => 10,20,50,100
    public string gameStatus;

    [Header("Card Properties")]
    public GameObject[] cardPlayer;    //card player
    public GameObject[] bgcardPlayer;  //background card
    public int[] typeCard;
    public float[] scoreCard;
    public SpriteRenderer bgscore;
    public SpriteRenderer Winner;
    public GameObject X2X3;
    public TextMesh score;  //score in round
    public string checkSort;   //เก็บค่าตัวเลขที่ได้เป็น String เพื่อเช็คหาไพ่เรียง

    public int getText;     //player get X2 X3 X5 (2 3 5)
    public float getRole;     //Role 1 = normal point,  2 = *2 point, 3 = *3 point, 4 = ไพ่เรียง ไพ่เซียน, 5 = ไพ่ตอง, 8 = pok8, 9 = pok9
    public bool requestCard;   //player4 request card when score greater than 3



    void Start()
    {
        current = this;
    }

    void LateUpdate()
    {
        GetScore();   //calculate score

        ShowUIScoreText(getText); //show all text user
        ShowUIProfileText();
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

    public void UnActiveAniamtion() //Active Animation
    {
        for (int i = 0; i < 3; i++)
        {
            cardPlayer[i].GetComponent<Animator>().enabled = false;
            bgcardPlayer[i].GetComponent<Animator>().enabled = false;
        }

    }

    public void GetScore() // +- score
    {
        if (totalScore >= 10) //score greater than 9 => -10
            totalScore -= 10;
        else if(totalScore >= 20) //score greater than 18 => -20
            totalScore -= 20;
    }


    public void ShowUIScoreText(int changeText)
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

    public void ShowUIProfileText()
    {
        money.text = "" + totalMoney;
    }

    
}
