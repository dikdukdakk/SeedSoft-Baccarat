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
        //textScoreHost.text = "" + PokdengCalculate.current.totalScore + " แต้ม";
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
        
    }

    public void BTSTART()
    {
        PokdengManagerBOT.current.drawCard = 1;
    }

    public void BTPass()
    {/*
        foreach (GameObject _player in PokdengManagerBOT.current.player)
        {
            _player.GetComponent<Player>().bgc1.SetActive(false);
            _player.GetComponent<Player>().cp1[].SetActive(false);
        }

        PokdengManagerBOT.current.host.bgch1.SetActive(false);
        PokdengManagerBOT.current.host.bgch2.SetActive(false);
     */

        /*
        if (PlayerCalculate.current.totalScore > PokdengCalculate.current.totalScore)
            PokdengUIManager.current.textStatus.text = "คุณชนะ";
        else if (PlayerCalculate.current.totalScore < PokdengCalculate.current.totalScore)
            PokdengUIManager.current.textStatus.text = "คุณแพ้";
        else
            PokdengUIManager.current.textStatus.text = "เสมอ";
        */
    }
}
