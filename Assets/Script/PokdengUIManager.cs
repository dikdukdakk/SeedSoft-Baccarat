using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
        PokdengManagerBOT.current.drawCard = 3;
        PokdengManagerBOT.current.player.ElementAt(4).requestCard = true;
        for (int i = 0; i < 9; i++)
            PokdengManagerBOT.current.player.ElementAt(i).score.enabled = true;
    }

    public void BTPass()
    {
        PokdengManagerBOT.current.drawCard = 3;
    }

    public void BTSTART()
    {
        PokdengManagerBOT.current.drawCard = 1;
    }

    
}
