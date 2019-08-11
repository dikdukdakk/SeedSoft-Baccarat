using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PokdengUIManager : MonoBehaviour
{
    public static PokdengUIManager current;

    [Header("All of UI")]
    public Image UIbgtimer;
    public Text UItexttimer;
    public GameObject BTdraw3;
    public GameObject BTpass;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    public void ActiveBTDraworPass()
    {
        BTdraw3.SetActive(true);
        BTpass.SetActive(true);
    }

    public void UnActiveBTDraworPass()
    {
        BTdraw3.SetActive(false);
        BTpass.SetActive(false);
    }

    public void BTPlus3()
    {
       // PokdengManagerBOT.current.player.ElementAt(4).requestCard = true;
    }

    public void BTPass()
    {
       // PokdengManagerBOT.current.player.ElementAt(4).requestCard = false;
    }

    
}
