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

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    public void BTPlus3()
    {
        PokdengManagerBOT.current.player.ElementAt(4).requestCard = true;
    }

    public void BTPass()
    {
        PokdengManagerBOT.current.player.ElementAt(4).requestCard = false;
        PokdengManagerBOT.current.drawCard = 3;
    }

    
}
