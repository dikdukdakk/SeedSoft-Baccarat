using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PokdengUIManager : MonoBehaviour
{
    public static PokdengUIManager current;

    [Header("Count of Card")]
    public Text textCountCard;

    [Header("All of Text")]
    public Text textScoreHost;
    public Text textScorePlayer;
    public Text textStatus;

    [Header("BG Card")]
    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    { 
        UITextScoreHost();
        UITextScorePlayer();
    }

   

    public void UITextScoreHost()
    {
        textScoreHost.text = "" + PokdengCalculate.current.totalScore + " แต้ม";
    }


    public void UITextScorePlayer()
    {
         //textScorePlayer.text = "" + PlayerCalculate.current.totalScore + " แต้ม";
    }



    public void BTRandom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BTPlus3()
    {
        //Player.current.c3.SetActive(true);
       // Player.current.countRandom = 4;
    }

    public void BTSTART()
    {
        HostCard.current.countRandom = 1;
       // Player.current.countRandom = 1;
    }

    public void BTPass()
    {
        BG1.SetActive(false);
        BG2.SetActive(false);
        BG3.SetActive(false);

        if (PlayerCalculate.current.totalScore > PokdengCalculate.current.totalScore)
            PokdengUIManager.current.textStatus.text = "คุณชนะ";
        else if (PlayerCalculate.current.totalScore < PokdengCalculate.current.totalScore)
            PokdengUIManager.current.textStatus.text = "คุณแพ้";
        else
            PokdengUIManager.current.textStatus.text = "เสมอ";
    }
}
