using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PokdengManagerBOT : MonoBehaviour
{
    public static PokdengManagerBOT current;

    [Header("Card Proproties")]
    public bool isGameStatus;
    public int drawCard; //  draw = 1 เท่ากับ 1 ใบ
    public List<Sprite> card = new List<Sprite>();
    public List<int> firstCard = new List<int>();
    public List<int> secondCard = new List<int>();
    public List<int> thirdCard = new List<int>();

    System.Random rnd = new System.Random();

    List<int> deck = new List<int>();

    [Header("Host Properties")]
    public HostCard host;

    [Header("Player Properties")]
    public List<Player> player = new List<Player>();

    public List<GameObject> _player = new List<GameObject>();

    void Start()
    {
        current = this;

        isGameStatus = true;
        drawCard = 0;

        deck = Enumerable.Range(0, 52).OrderBy(c => rnd.Next()).ToList();
    }

    void Update()
    {
        GameStatus(isGameStatus);
    }

    public void GameStatus(bool gameStatus)
    {
        if (gameStatus != true)
            return;

        RandomCard();
    }

    public void RandomCard()
    {   
        if (drawCard == 1)
        {
            for (int i = 0; i < 10; i++)
                firstCard.Add(deck.ElementAt(i));

            //Player Card
            foreach (Player _player in player)
            {
                _player.cp1.SetActive(true);
                

                _player.bgc1.SetActive(true);

                for (int i = 0; i < 9; i++)
                    _player.cp1.GetComponent<Image>().sprite = card[firstCard[i]];
            }
            

            /*
            for(int i=0; i< 10; i++)
            {
                _player.GetComponent<Player>().cp1[i].SetActive(true);
                _player.GetComponent<Player>().bgc1[i].SetActive(true);
                _player.GetComponent<Player>().cp1[i].GetComponent<Image>().sprite = card[firstCard[i]];
            }*/

            //Host Card
            host.bgch1.SetActive(true);
            host.ch1.SetActive(true);
            host.ch1.GetComponent<Image>().sprite = card[firstCard[9]];
            
            //next draw card
            drawCard = 2;
        }

        else if(drawCard == 2)
        {
            for (int i = 10; i < 20; i++)
                secondCard.Add(deck.ElementAt(i));

            //Player Card
            foreach (Player _player in player)
            {
                _player.cp2.SetActive(true);
                _player.bgc2.SetActive(true);

                for(int i=0; i<9;i++)
                    _player.cp2.GetComponent<Image>().sprite = card[secondCard[i]];
            }


            //Host Card
            host.bgch2.SetActive(true);
            host.ch2.SetActive(true);
            host.ch2.GetComponent<Image>().sprite = card[secondCard[9]];

            //Next draw card
            drawCard = 0;
        }

        else if(drawCard == 3)
        {
            
        }
    }

    IEnumerator waitDrawCard2()
    {
        yield return new WaitForSeconds(2f);
    }

}
