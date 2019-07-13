using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardAnimation : MonoBehaviour
{
    public void openCard1()
    {
        PokdengManagerBOT.current.player.ElementAt(4).bgc1.SetActive(false);
        
    }

    public void openCard2()
    {
        PokdengManagerBOT.current.player.ElementAt(4).bgc2.SetActive(false);
    }

    public void openCard3()
    {
        PokdengManagerBOT.current.player.ElementAt(4).bgc3.SetActive(false);
    }
}
