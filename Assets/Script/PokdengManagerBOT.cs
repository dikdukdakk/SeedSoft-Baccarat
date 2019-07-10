using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokdengManagerBOT : MonoBehaviour
{
    public static PokdengManagerBOT current;

    [Header("Game Proproties")]
    public bool isGameStatus;
    public int drawCard; //  draw = 1 เท่ากับ 1 ใบ
    public List<Sprite> card = new List<Sprite>();
    public int[] firstCard, secondCard, thirdCard;

    [Header("Host properties")]
    public GameObject ch1; // card host
    public GameObject ch2;
    public GameObject ch3;
    public int cardHost1, cardHost2, cardHost3;

    [Header("Player properties")]
    public List<Player> player = new List<Player>();

    int[] check1;

    void Start()
    {
        current = this;

        drawCard = 0;


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
            for (int i = 0; i < firstCard.Length; i++)
            {
                firstCard[i] = Random.Range(0, 51);
            }

            drawCard = 2;
        }

        else if(drawCard == 2)
        {
            for (int i = 0; i < secondCard.Length; i++)
            {
                secondCard[i] = Random.Range(0, 51);
            }

            drawCard = 0;
        }

        else if(drawCard == 3)
        {
            for (int i = 0; i < thirdCard.Length; i++)
            {
                thirdCard[i] = Random.Range(0, 51);
            }
        }
    }

}
