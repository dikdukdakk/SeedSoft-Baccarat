using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Count of Card")]
    public Text textCountCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UICountOfCard();
    }

    public void UICountOfCard()
    {
        textCountCard.text = "" + GameManager.current.countCard;
    }

    public void BTRandom()
    {
        GameManager.current.countRandom = 0;
    }

    public void BTPlus3()
    {
        GameManager.current.c3.SetActive(true);
    }
}
