using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpenCard : MonoBehaviour
{
    public static OpenCard current;

    [Header("Open Card")]
    public GameObject activeCardAnimation;
    public SkinnedMeshRenderer plane1,plane2, plane3;
    public Animator card2, card3;


    // Start is called before the first frame update
    void Start()
    {
        current = this;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //กำหนดการ์ดใบที่ 1 2 3 ของผู้เล่นที่สุ่มได้
        if (PokdengManagerBOT.current.player.ElementAt(4).bgcardPlayer[0].active)
        {
            
            //plane1.materials[0].SetTexture = new Material(PokdengManagerBOT.current.MaterialOfCard[3]);
        }
        
        //if (PokdengManagerBOT.current.player.ElementAt(4).bgcardPlayer[1].active)
        //    plane2.materials[0] = PokdengManagerBOT.current.MaterialOfCard[PokdengManagerBOT.current.secondCard[4]];
        //if (PokdengManagerBOT.current.player.ElementAt(4).bgcardPlayer[2].active)
        //    plane3.materials[0] = PokdengManagerBOT.current.MaterialOfCard[PokdengManagerBOT.current.thirdCard[4]];
    
    }
}
