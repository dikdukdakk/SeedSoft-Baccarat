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

    [Header("Card Properties")]
    public GameObject X2X3;
    public GameObject cardPlayer1;
    public GameObject cardPlayer2;  //card player in one round
    public GameObject cardPlayer3;
    public GameObject bgc1, bgc2, bgc3;  //bg card
    public int typeCard1, typeCard2, typeCard3;
    public float scoreCard1, scoreCard2, scoreCard3;
    
    public bool requestCard;   //player4 request card when score greater than 3



    void Start()
    {
        current = this;
    }

    void LateUpdate()
    {
        GetScore();   //calculate score
        //GetStar();

        ShowUIText(); //show all text user
    }

    public void ActiveAniamtion(int cntCard) //Active Animation
    {
        cardPlayer1.GetComponent<Animator>().enabled = true;
        bgc1.GetComponent<Animator>().enabled = true;
        cardPlayer2.GetComponent<Animator>().enabled = true;
        bgc2.GetComponent<Animator>().enabled = true;

        if (cntCard == 3)
        {
            cardPlayer3.GetComponent<Animator>().enabled = true;
            bgc3.GetComponent<Animator>().enabled = true;
        }
              
    }

    public void GetScore() // +- score
    {
        if (totalScore >= 10) //score greater than 9 => -10
            totalScore -= 10;
        else if(totalScore >= 20) //score greater than 18 => -20
            totalScore -= 20;
    }



    public void ShowUIText()
    {
        score.text = "" + totalScore + " แต้ม";
    }

}
