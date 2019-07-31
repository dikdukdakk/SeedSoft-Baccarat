using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCard : MonoBehaviour
{
    public static OpenCard current;

    [Header("Open Card2")]
    public GameObject activeCardAnimation1;
    public SkinnedMeshRenderer plane11,plane12;
    public GameObject card2;

    [Header("Open Card3")]
    public GameObject activeCardAnimation2;
    public SkinnedMeshRenderer plane21,plane22,plane23;
    public GameObject card3;

    // Start is called before the first frame update
    void Start()
    {
        current = this; 
    }

    private void Update()
    {
        plane21.material = plane11.material;
        plane22.material = plane12.material;
    }

    public void AnimCard2_1()
    {
        card2.GetComponent<Animator>().SetFloat("PlayandStop", 1f);
    }

    public void UnAnimCard2_1()
    {
        card2.GetComponent<Animator>().SetFloat("PlayandStop", 0f);
    }



}
