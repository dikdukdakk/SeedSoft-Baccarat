using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [Header("Properties of Card")]
    public int countCard;
    public int countRandom = 0;
    private int numCard1, numCard2, numCard3;
    public List<Sprite> card = new List<Sprite>();

    [Header("Player Card")]
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;

    void Start()
    {
        current = this;
        countRandom = 0;
    }

   
    void Update()
    {
        countCard = card.Count;

        RandomCard();
    }

    public void RandomCard()
    {
        if(countRandom == 0)
        {
            c1.SetActive(true);

            numCard1 = Random.Range(0, 51);
            c1.GetComponent<SpriteRenderer>().sprite = card[numCard1];

            countRandom = 1;
        }

        else if(countRandom == 1)
        {
            c2.SetActive(true);

            numCard2 = Random.Range(0, 51);
            if (numCard1 != numCard2)
            {
                c2.GetComponent<SpriteRenderer>().sprite = card[numCard2];
                countRandom = 3;
            }
            else
                numCard2 = Random.Range(0, 51);

        }
        else if(countRandom == 3)
        {
            numCard3 = Random.Range(0, 51);
            if (numCard1 != numCard2 && numCard1 != numCard3 && numCard2 != numCard3)
            {
                c3.GetComponent<SpriteRenderer>().sprite = card[numCard3];
                countRandom = 4;
            }
            else
                numCard3 = Random.Range(0, 51);
        }

    }

}
