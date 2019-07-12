using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokdengCalculate : MonoBehaviour
{
    public static PokdengCalculate current;

    [Header("Score of Card")]
    public List<int> totalScore = new List<int>();

    void Start()
    {
        current = this;
    }

    void Update()
    {

    }

   
}
