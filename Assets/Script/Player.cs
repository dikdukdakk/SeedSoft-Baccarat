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
    public GameObject cardPlayer1;
    public GameObject cardPlayer2;  //card player in one round
    public GameObject cardPlayer3;
    public GameObject bgc1, bgc2, bgc3;  //bg card
    public int typeCard1, typeCard2, typeCard3;
    public GameObject X2X3;
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
