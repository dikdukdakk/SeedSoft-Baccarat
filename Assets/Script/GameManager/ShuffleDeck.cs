using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class ShuffleDeck : MonoBehaviour
{
    [Header("Card Properties")]
    public List<Sprite> card = new List<Sprite>(); // รูปการ์ดทั้งหมด
    public List<int> firstCard = new List<int>();  // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 1
    public List<int> secondCard = new List<int>(); // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 2
    public List<int> thirdCard = new List<int>();  // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 3

    System.Random rnd = new System.Random();       // ตัวรันลำดับ random ใน list
    List<int> deck = new List<int>();              // จัด deck

    public static ShuffleDeck toStatic;

    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnShuffleDeck(bool shuffe)
    {
        if (!shuffe)
            return;

        firstCard.Clear();  //reset list of card1,2,3
        secondCard.Clear();
        thirdCard.Clear();

        //shuffle deck
        deck = Enumerable.Range(0, 52).OrderBy(c => rnd.Next()).ToList();  // สลับตำแหน่ง 0, 52 ให้ตำแหน่งไม่เรียงกัน

        for (int i = 0; i < 10; i++)
            firstCard.Add(deck.ElementAt(i));   // เก็บค่าไพ่ที่ได้ลำดับที่ 1-9 ไว้ใน ตัวแปรการ์ดใบที่ 1

        for (int i = 10; i < 20; i++)
            secondCard.Add(deck.ElementAt(i));  // เก็บค่าไพ่ที่ได้ลำดับที่ 10-19 ไว้ใน ตัวแปรการ์ดใบที่ 2

        for (int i = 20; i < 30; i++)
            thirdCard.Add(deck.ElementAt(i));   // เก็บค่าไพ่ที่ได้ลำดับที่ 10-19 ไว้ใน ตัวแปรการ์ดใบที่ 3

        // StartCoroutine(waitShuffleDeck());
        PKManager.toStatic.isShuffledeck = false;
    }
    //IEnumerator waitShuffleDeck()
    //{
    //    yield return new WaitForSeconds(1);
    //    PKManager.toStatic.isShuffledeck = false;
    //}
}
