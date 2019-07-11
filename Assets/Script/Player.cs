using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player current;

    [Header ("Player Info")]
    public Text username;
    public Text money;
    public Image photo;
    public int totalMoney;
    public int totalPoint;

    [Header("Card GameObject")]
    public GameObject cp1;
    public GameObject cp2, cp3;
    public GameObject bgc1, bgc2, bgc3;

    void Start()
    {
        current = this;
    }

    
}
