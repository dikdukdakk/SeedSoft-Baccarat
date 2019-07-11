using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostCard : MonoBehaviour
{
    public static HostCard current;

    [Header("Host Info")]
    public Text username;
    public Text money;
    public Image photo;
    public int totalMoney;
    public int totalPoint;

    [Header("Card GameObject")]
    public GameObject ch1; //card host
    public GameObject ch2,ch3; 
    public GameObject bgch1, bgch2, bgch3; //background card player



}
