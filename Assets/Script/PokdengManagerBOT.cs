using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using System.Linq;      // เพิ่มการทำงานให้ list สามารถอ้างอิงเชิงลึกได้

public class PokdengManagerBOT : MonoBehaviour
{
    public static PokdengManagerBOT current;       // ใช้ current อ้างอิงไปยังไฟล์อื่น

    [Header("Card Proproties")]
    public bool isGameStatus;                      // สถานะเกม
    public int drawCard;                           // รอบการ์ดที่แจก
    public List<Sprite> card = new List<Sprite>(); // รูปการ์ดทั้งหมด
    public List<int> typeCard = new List<int>();   // type of card => Club, Diamond, Heart, Spade
    public List<string> sorf = new List<string>(); // เก็บตัวเลขเรียงทั้งหมด
    public List<float> scoreCard = new List<float>();  // คะแนนการ์ดทั้งหมด อ้างอิงลำดับจาก list card
    public List<int> firstCard = new List<int>();  // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 1
    public List<int> secondCard = new List<int>(); // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 2
    public List<int> thirdCard = new List<int>();  // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 3

    public List<Sprite> X2X3Card = new List<Sprite>(); //รูป x2, x3

    System.Random rnd = new System.Random();       // ตัวรันลำดับ random ใน list

    List<int> deck = new List<int>();              // จัด deck

    [Header("Host Properties")]
    public HostCard host;                          // อ้างอิง scirpt host

    [Header("Player Properties")]
    public List<Player> player = new List<Player>();  // อ้างอิง script player

    void Start()
    {//Start
        current = this;

        isGameStatus = true;   // เมื่อเริ่มเกม ให้สถานะเกมเท่ากับ true 
        drawCard = 0;          // กำหนดให้ drawcard = 0, ยังไม่เริ่มแจกการ์ด

        //shuffle deck
        deck = Enumerable.Range(0, 52).OrderBy(c => rnd.Next()).ToList();  // สลับตำแหน่ง 0, 52 ให้ตำแหน่งไม่เรียงกัน

        for (int i = 0; i < 10; i++)
            firstCard.Add(deck.ElementAt(i));   // เก็บค่าไพ่ที่ได้ลำดับที่ 1-9 ไว้ใน ตัวแปรการ์ดใบที่ 1

        for (int i = 10; i < 20; i++)
            secondCard.Add(deck.ElementAt(i));  // เก็บค่าไพ่ที่ได้ลำดับที่ 10-19 ไว้ใน ตัวแปรการ์ดใบที่ 2

        for (int i = 20; i < 30; i++)
            thirdCard.Add(deck.ElementAt(i));   // เก็บค่าไพ่ที่ได้ลำดับที่ 10-19 ไว้ใน ตัวแปรการ์ดใบที่ 3


    }// end start

    void FixedUpdate()
    {//FixedUpdate
        GameStatus(isGameStatus); //ส่ง isGameStatus เข้าไปเช็คในฟังก์ชัน

    }//end fixedUpdate

    public void GameStatus(bool gameStatus)
    {//GameStatus
        if (gameStatus != true)  //ถ้าค่าที่รับเข้ามาเป็น false ให้ออก , เกมจบแล้วให้ออก
            return;

        StartCoroutine(RandomCard()); //เริ่มเกม



    }//end GameStatus

    IEnumerator RandomCard()
    {//Randomcard
        if (drawCard == 1) // card = 1
        {
            //Player Data
            for (int i = 0; i < 9; i++)
            {//draw card of 1-8
                player.ElementAt(i).cardPlayer1.SetActive(true);  //active image card
                player.ElementAt(i).cardPlayer1.GetComponent<Image>().sprite = card[firstCard[i]];  //active image card
                player.ElementAt(i).bgc1.SetActive(true);  //active bg card

                player.ElementAt(i).typeCard1 = typeCard[firstCard[i]];
                player.ElementAt(i).scoreCard1 = scoreCard[firstCard[i]];

               
                yield return new WaitForSeconds(0.2f); //delay draw card
            }//end draw card of 1-8

            //Host Data
            host.bgch1.SetActive(true); //active bg card
            host.ch1.SetActive(true);   //active card
            host.ch1.GetComponent<Image>().sprite = card[firstCard[9]]; //active image card
            host.typeCard1 = typeCard[firstCard[9]];

            //next draw card
            drawCard = 2;
        }

        else if (drawCard == 2) //card = 2
        {
            //Player Data
            for (int i = 0; i < 9; i++)
            {//loop
                player.ElementAt(i).cardPlayer2.SetActive(true);
                player.ElementAt(i).cardPlayer2.GetComponent<Image>().sprite = card[secondCard[i]];
                player.ElementAt(i).bgc2.SetActive(true);
                player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]] + (int)scoreCard[secondCard[i]]; // + score card 1, 2
                
                player.ElementAt(i).typeCard2 = typeCard[secondCard[i]];
                player.ElementAt(i).scoreCard2 = scoreCard[secondCard[i]];

                yield return new WaitForSeconds(0.2f);
            }//end loop

            //Host Card
            host.bgch2.SetActive(true);
            host.ch2.SetActive(true);
            host.ch2.GetComponent<Image>().sprite = card[secondCard[9]];
            host.totalScore = (int)scoreCard[firstCard[9]] + (int)scoreCard[secondCard[9]];  // + score card 1, 2
            host.typeCard2 = typeCard[secondCard[9]];

            //หลังจากแจกการ์ดถ้าผู้เล่นหรือเจ้าได้ 8 และ 9 การ์ดจะเปิดเอง
            StartCoroutine(Calculate(drawCard));
            drawCard = 0;
   
        }

        else if (drawCard == 3) //card = 3
        {
            //player data
            for (int i = 0; i < 9; i++)
            {
                if (player.ElementAt(i).totalScore < 4) // score less than 4 auto draw card
                {//if else
                    player.ElementAt(i).cardPlayer3.SetActive(true);
                    player.ElementAt(i).cardPlayer3.GetComponent<Image>().sprite = card[thirdCard[i]];
                    player.ElementAt(i).bgc3.SetActive(true);
                    player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]]
                                                   + (int)scoreCard[secondCard[i]] // + score card 1, 2 , 3
                                                   + (int)scoreCard[thirdCard[i]];
                
                    player.ElementAt(i).typeCard3 = typeCard[thirdCard[i]];
                    player.ElementAt(i).scoreCard3 = scoreCard[thirdCard[i]];

                    player.ElementAt(i).checkSort = "" + scoreCard[firstCard[i]].ToString() + " " + scoreCard[secondCard[i]].ToString() + " " + scoreCard[thirdCard[i]].ToString(); //เก็บค่าไพ่ที่ได้เป็นให้อยู่ในรูปแบบ String 

                    yield return new WaitForSeconds(0.2f);
                }//end if

                if (player.ElementAt(i).requestCard == true) //when player want to draw card
                {//if else
                    player.ElementAt(i).cardPlayer3.SetActive(true);
                    player.ElementAt(i).cardPlayer3.GetComponent<Image>().sprite = card[thirdCard[i]];
                    player.ElementAt(i).bgc3.SetActive(true);
                    player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]]
                                                   + (int)scoreCard[secondCard[i]]
                                                   + (int)scoreCard[thirdCard[i]];

                    player.ElementAt(i).typeCard3 = typeCard[thirdCard[i]];
                    player.ElementAt(i).scoreCard3 = scoreCard[thirdCard[i]];

                    
                    player.ElementAt(i).checkSort = "" + scoreCard[firstCard[i]].ToString() +
                                                    " " + scoreCard[secondCard[i]].ToString() +
                                                    " " + scoreCard[thirdCard[i]].ToString();   
                                                    
                    yield return new WaitForSeconds(0.2f);
                }//end if


            }

            //host data
            if (host.totalScore < 5) // score less than 4 auto draw card
            {//if else
                host.bgch3.SetActive(true);
                host.ch3.SetActive(true);
                host.ch3.GetComponent<Image>().sprite = card[thirdCard[9]];
                host.totalScore = host.totalScore + (int)scoreCard[thirdCard[9]];
                host.typeCard3 = typeCard[thirdCard[9]];
            }//end if

            //การ์ดทั้งหมดจะถูกเปิดออก
            StartCoroutine(Calculate(drawCard));
            drawCard = 0; //stop draw card

        }


    }//end RandomCard

    IEnumerator Calculate(int cntCard)
    {//Calculate2card
        yield return new WaitForSeconds(2); //Delay draw card 2 sec
        if (cntCard == 2)
        {
            //auto open card AI, when ai get pok 8,9    
            for (int i = 0; i < 9; i++)
            {//loop
                if (player.ElementAt(i) != player.ElementAt(4)) //player 4 คือ ผู้เล่น
                {//if
                    if (player.ElementAt(i).totalScore == 8 || player.ElementAt(i).totalScore == 9) //pok 8,9
                    {
                        player.ElementAt(i).ActiveAniamtion(cntCard);
                        player.ElementAt(i).score.enabled = true;      //active score text
                    }

                    // เช็ค 2 เด้ง
                    if (player.ElementAt(i).typeCard1 == player.ElementAt(i).typeCard2 ||
                        player.ElementAt(i).scoreCard1 == player.ElementAt(i).scoreCard2) //ถ้าดอกใบที่ 1 และ 2 เหมือนกัน
                        player.ElementAt(i).X2X3.SetActive(true); //active x2
                }//end if
            }//end loop

            yield return new WaitForSeconds(2);   //Delay draw card 1 sec                                               
            if (host.totalScore == 8 || host.totalScore == 9) //auto open all player, when host get pok 8,9
            {//if else
                host.ActiveAniamtion(cntCard);
                for (int i = 0; i < 9; i++)
                {
                    player.ElementAt(i).ActiveAniamtion(cntCard); //call ActiveAnimation in Player.cs
                    player.ElementAt(i).score.enabled = true;      //active score text
                }
            }//end if
   
            if (host.typeCard1 == host.typeCard2)
                host.X2X3.SetActive(true);
        }
        else if (cntCard == 3)
        {//if else
            yield return new WaitForSeconds(2);
            host.ActiveAniamtion(cntCard); //call ActiveAnimation in host.cs
            host.score.enabled = true;   //active score text 
            if(host.ch3.active) //ถ้า การ์ดใบที่ 3 เปิด ให้ทำงาน if นี้
                host.X2X3.SetActive(false); //unactive x2x3 gameobject
            if (host.typeCard1 == host.typeCard2 && host.typeCard1 == host.typeCard3 && 
                host.typeCard2 == host.typeCard3){  //if ถ้าดอกที่ 1,2,3 เหมือนกัน
                host.X2X3.SetActive(true);  //active X2X3
                host.X2X3.GetComponent<Image>().sprite = X2X3Card[1]; //change image x2 to x3
            }//end if
        
            for (int i = 0; i < 9; i++)
            {//loop
                player.ElementAt(i).ActiveAniamtion(cntCard);  //active animation
                player.ElementAt(i).score.enabled = true;      //active score text
                if (player.ElementAt(i).cardPlayer3.active)
                    player.ElementAt(i).X2X3.SetActive(false);

                // เช็ค 3 เด้ง (ไพ่ดอกเหมือนกัน *3 และ ไพ่เซียน)
                if ((player.ElementAt(i).typeCard1 == player.ElementAt(i).typeCard2 &&
                    player.ElementAt(i).typeCard1 == player.ElementAt(i).typeCard3 &&
                    player.ElementAt(i).typeCard2 == player.ElementAt(i).typeCard3) || 
                   (player.ElementAt(i).scoreCard1 > 0 && player.ElementAt(i).scoreCard1 <= .9 &&
                    player.ElementAt(i).scoreCard2 > 0 && player.ElementAt(i).scoreCard2 <= .9 &&
                    player.ElementAt(i).scoreCard3 > 0 && player.ElementAt(i).scoreCard3 <= .9)){ //if else
                    player.ElementAt(i).X2X3.SetActive(true); 
                    player.ElementAt(i).X2X3.GetComponent<Image>().sprite = X2X3Card[1];
                }//end if

                for (int j = 0; j < 66; j++)
                {
                    if (i != j)
                    {
                        if (player.ElementAt(i).checkSort == sorf[j])
                        {
                            player.ElementAt(i).X2X3.SetActive(true);
                            player.ElementAt(i).X2X3.GetComponent<Image>().sprite = X2X3Card[2];
                        }
                        Debug.Log("" + j);
                    }                    
                }

                // เช็ค 5 เด้ง (ไพ่ตอง)
                if (player.ElementAt(i).scoreCard1 == player.ElementAt(i).scoreCard2 &&
                    player.ElementAt(i).scoreCard1 == player.ElementAt(i).scoreCard3 &&
                    player.ElementAt(i).scoreCard2 == player.ElementAt(i).scoreCard3){ //if else
                    player.ElementAt(i).X2X3.SetActive(true);
                    player.ElementAt(i).X2X3.GetComponent<Image>().sprite = X2X3Card[2];
                }//end if
            }//end loop
        } //end if
    }//end Calculate2card

}
