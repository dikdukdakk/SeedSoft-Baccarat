using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BetMoney : MonoBehaviour
{
    public static BetMoney current;

    //[Header(" Money")
    public List<int> money = new List<int>();
    public int[] moneybet;

    private void Start()
    {
        current = this;
    }

    //Player get bet
    public void Bet(int bet, Player player)
    {
        player.totalMoney = player.totalMoney - bet;
    }




    public void ButtonBet1()
    {
        
    }
}
