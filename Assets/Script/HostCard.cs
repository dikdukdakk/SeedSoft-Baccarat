using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostCard : MonoBehaviour
{
    public static HostCard current;

    [Header("Host Card")]
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;

    [Header("Properties of Card")]
    public int countRandom = 0;
    public int numCard1, numCard2, numCard3;
    private int amountCard;

    void Start()
    {
        current = this;
        countRandom = 0;
    }

    void Update()
    {
        //RandomCard();

        if (countRandom > 1)
            PokdengCalculate.current.GetScore(amountCard);
    }

    
    public void RandomCard()
    {
        if (countRandom == 1)
        {
            c1.SetActive(true);

            numCard1 = Random.Range(0, 51);
            c1.GetComponent<SpriteRenderer>().sprite = PokdengManagerBOT.current.card[numCard1];

            PokdengManagerBOT.current.card[numCard1] = null;

            countRandom = 2;
            amountCard = 1;
        }

        else if (countRandom == 2)
        {
            
            c2.SetActive(true);

            numCard2 = Random.Range(0, 51);
            if (numCard1 != numCard2)
            {
                c2.GetComponent<SpriteRenderer>().sprite = PokdengManagerBOT.current.card[numCard2];

                PokdengManagerBOT.current.card[numCard2] = null;

                countRandom = 3;
                amountCard = 2;
            }
            else
                numCard2 = Random.Range(0, 51);
            

        }

        else if (PokdengCalculate.current.totalScore < 5 && countRandom == 3)
        {

            numCard3 = Random.Range(0, 51);
            if (numCard1 != numCard2 && numCard1 != numCard3 && numCard2 != numCard3)
            {
                Invoke("waitDrawCard", 2);
                countRandom = 4;
            }
            else
                numCard3 = Random.Range(0, 51);
        }

    }
    
    void waitDrawCard()
    {
        c3.SetActive(true);
        c3.GetComponent<SpriteRenderer>().sprite = PokdengManagerBOT.current.card[numCard3];

        PokdengManagerBOT.current.card[numCard3] = null;

        amountCard = 3;
    }
    
    
}
