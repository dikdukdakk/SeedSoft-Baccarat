using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player current;

    public Text username;
    public Text money;
    public Image photo;

    public int totalMoney;
    public int totalPoint;

    public GameObject cp1, cp2, cp3; //card player


    void Start()
    {
        current = this;
    }
}
