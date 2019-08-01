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
    public List<Material> MaterialOfCard = new List<Material>();
    public List<int> typeCard = new List<int>();   // type of card => Club, Diamond, Heart, Spade
    public List<string> sorf = new List<string>(); // เก็บไพ่เรียงทั้งหมดในรูปแบบ string เช่น 2 3 4, 4 5 6 
    public List<float> scoreCard = new List<float>();  // คะแนนการ์ดทั้งหมด อ้างอิงลำดับจาก list card
    public List<int> firstCard = new List<int>();  // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 1
    public List<int> secondCard = new List<int>(); // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 2
    public List<int> thirdCard = new List<int>();  // เก็บคะแนน และรูปการ์ดที่สุ่มได้ ใบที่ 3

    public List<Sprite> X2X3Card = new List<Sprite>(); //รูป x2, x3

    bool shuffledeck;
    System.Random rnd = new System.Random();       // ตัวรันลำดับ random ใน list
    List<int> deck = new List<int>();              // จัด deck

    [Header("Host Properties")]
    public HostCard host;                          // อ้างอิง scirpt host

    [Header("Player Properties")]
    public List<Player> player = new List<Player>();  // อ้างอิง script player

    void Start()
    {//Start
        current = this;        //ให้ current สามารถอ้างอิงถึงสคิปนี้ได้

        isGameStatus = true;   //เมื่อเริ่มเกม ให้สถานะเกมเท่ากับ true 
        shuffledeck = true;    //เริ่มสับไพ่

        GetmoneytoPlayer();

    }// end start

    void FixedUpdate()
    {//FixedUpdate
        
        GameStatus(isGameStatus); //ส่ง isGameStatus เข้าไปเช็คในฟังก์ชัน

    }//end fixedUpdate


    public void GameStatus(bool gameStatus)
    {//GameStatus
        if (!gameStatus)  //ถ้าค่าที่รับเข้ามาเป็น false ให้ออก , เกมจบแล้วให้ออก
            return;

        ShuffleDeck(shuffledeck); //สลับการ์ด
        StartCoroutine(RandomCard()); //เริ่มเกม

    }//end GameStatus

    void GetmoneytoPlayer()
    { 
        for(int i=0; i < 9; i++)
        {
            if(player.ElementAt(4) != player.ElementAt(i))
            { 
                int randomMoney = Random.Range(0, 5);
                player.ElementAt(i).totalMoney = BetMoney.current.money[randomMoney];
            }

        }
    }

    void ShuffleDeck(bool newGame)
    {
        if (!newGame)
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

        StartCoroutine(waitShuffleDeck());
    }
    IEnumerator waitShuffleDeck()
    {
        yield return new WaitForSeconds(1);
        shuffledeck = false;
        drawCard = 1; //เริ่้มแจกการ์ดใบที่ 1
    }

    IEnumerator RandomCard()
    {//Randomcard
        if (drawCard == 1) // card = 1
        {//if
            //Player Data
            for (int i = 0; i < 9; i++)
            {//draw card of 0-8
                player.ElementAt(i).cardPlayer[0].SetActive(true);  //active image card
                player.ElementAt(i).cardPlayer[0].GetComponent<SpriteRenderer>().sprite = card[firstCard[i]];  //active image card
                player.ElementAt(i).bgcardPlayer[0].SetActive(true);  //active bg card
                player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]];

                player.ElementAt(i).typeCard[0] = typeCard[firstCard[i]]; //set number of type card1
                player.ElementAt(i).scoreCard[0] = scoreCard[firstCard[i]]; //set number of score card

                yield return new WaitForSeconds(0.2f); //delay draw card
                
            }//end draw card of 0-8

            //Host Data
            host.bgcardHost[0].SetActive(true); //active bg card
            host.cardHost[0].SetActive(true);   //active card
            host.cardHost[0].GetComponent<SpriteRenderer>().sprite = card[firstCard[9]]; //active image card
            host.typeCard[0] = typeCard[firstCard[9]]; //set number of type card1
            host.scoreCard[0] = scoreCard[firstCard[9]]; //set number of score card

            

            //next draw card
            drawCard = 2;
        }//end if
        else if (drawCard == 2) //card = 2
        {//if
            //Player Data
            for (int i = 0; i < 9; i++)
            {//loop
                player.ElementAt(i).cardPlayer[1].SetActive(true);
                player.ElementAt(i).cardPlayer[1].GetComponent<SpriteRenderer>().sprite = card[secondCard[i]];
                player.ElementAt(i).bgcardPlayer[1].SetActive(true);
                player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]] + (int)scoreCard[secondCard[i]]; // + score card 1, 2
                
                player.ElementAt(i).typeCard[1] = typeCard[secondCard[i]];
                player.ElementAt(i).scoreCard[1] = scoreCard[secondCard[i]];

                yield return new WaitForSeconds(0.2f);
            }//end loop

            //Host Card
            host.bgcardHost[1].SetActive(true);
            host.cardHost[1].SetActive(true);
            host.cardHost[1].GetComponent<SpriteRenderer>().sprite = card[secondCard[9]];
            host.totalScore = (int)scoreCard[firstCard[9]] + (int)scoreCard[secondCard[9]];  // + score card 1, 2
            host.typeCard[1] = typeCard[secondCard[9]]; //set number of type card2
            host.scoreCard[1] = scoreCard[secondCard[9]];

            OpenCard.current.plane11.material = MaterialOfCard[firstCard[4]];
            OpenCard.current.plane12.material = MaterialOfCard[secondCard[4]];

            //หลังจากแจกการ์ดถ้าผู้เล่นหรือเจ้าได้ 8 และ 9 การ์ดจะเปิดเอง
            StartCoroutine(Calculate(drawCard));     
            drawCard = 0;
           
        }//end if

        else if (drawCard == 3) //card = 3
        {//if
            //player data
            for (int i = 0; i < 9; i++)
            {
                if (player.ElementAt(i).totalScore < 4) // score less than 4 auto draw card
                {//if else
                    player.ElementAt(i).cardPlayer[2].SetActive(true);
                    player.ElementAt(i).cardPlayer[2].GetComponent<SpriteRenderer>().sprite = card[thirdCard[i]];
                    player.ElementAt(i).bgcardPlayer[2].SetActive(true);
                    player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]]
                                                   + (int)scoreCard[secondCard[i]] // + score card 1, 2 , 3
                                                   + (int)scoreCard[thirdCard[i]];
                
                    player.ElementAt(i).typeCard[2] = typeCard[thirdCard[i]];
                    player.ElementAt(i).scoreCard[2] = scoreCard[thirdCard[i]];

                    player.ElementAt(i).requestCard = true;

                    //convert int to string for check sorf card ("2 3 4", "4 5 6")
                    player.ElementAt(i).checkSort = "" + scoreCard[firstCard[i]].ToString() + " " + scoreCard[secondCard[i]].ToString() + 
                                                    " " + scoreCard[thirdCard[i]].ToString();

                    yield return new WaitForSeconds(0.2f);
                }//end if

                if (player.ElementAt(i).requestCard == true) //when player want to draw card
                {//if else
                    player.ElementAt(i).cardPlayer[2].SetActive(true);
                    player.ElementAt(i).cardPlayer[2].GetComponent<SpriteRenderer>().sprite = card[thirdCard[i]];
                    player.ElementAt(i).bgcardPlayer[2].SetActive(true);
                    player.ElementAt(i).totalScore = (int)scoreCard[firstCard[i]]
                                                   + (int)scoreCard[secondCard[i]]
                                                   + (int)scoreCard[thirdCard[i]];

                    player.ElementAt(i).typeCard[2] = typeCard[thirdCard[i]];
                    player.ElementAt(i).scoreCard[2] = scoreCard[thirdCard[i]];

                    
                    player.ElementAt(i).checkSort = "" + scoreCard[firstCard[i]].ToString() +
                                                    " " + scoreCard[secondCard[i]].ToString() +
                                                    " " + scoreCard[thirdCard[i]].ToString();   
                                                    
                    yield return new WaitForSeconds(0.2f);
                }//end if

            }

            //host data
            if (host.totalScore < 5) //score less than 4 auto draw card
            {//if else
                host.bgcardHost[2].SetActive(true);
                host.cardHost[2].SetActive(true);
                host.cardHost[2].GetComponent<SpriteRenderer>().sprite = card[thirdCard[9]];
                host.totalScore = (int)scoreCard[firstCard[9]] + (int)scoreCard[secondCard[9]] + (int)scoreCard[thirdCard[9]];
                host.typeCard[2] = typeCard[thirdCard[9]];
                host.scoreCard[2] = scoreCard[thirdCard[9]];
                host.checkSort = "" + scoreCard[firstCard[9]].ToString() + " " + scoreCard[secondCard[9]].ToString() +  
                                 " " + scoreCard[thirdCard[9]].ToString();
            }//end if

            OpenCard.current.plane23.material = MaterialOfCard[thirdCard[4]];

            //setting delay
            StartCoroutine(Calculate(drawCard));
            drawCard = 0;
        }//end if


    }//end RandomCard
    IEnumerator Calculate(int cntCard)
    {//Calculate
        if (cntCard == 2)
        {//if cnt = 2

            //setting dealy 10 sec before draw card
            PokdengUIManager.current.UItexttimer.enabled = true;
            PokdengUIManager.current.UIbgtimer.enabled = true;
            PokdengUIManager.current.ActiveBTDraworPass();
            OpenCard.current.activeCardAnimation1.SetActive(true);
            for (int i = 10; i > 0; i--)
            {
                PokdengUIManager.current.UItexttimer.text = "" + i;
                yield return new WaitForSeconds(1f);
            } //UI timer text
            OpenCard.current.activeCardAnimation1.SetActive(false);
            player.ElementAt(4).ActiveAniamtion(cntCard);
            
            //auto open card AI, when ai get pok 8,9    
            for (int i = 0; i < 9; i++)
            {//loop
                player.ElementAt(i).getText = 1;
                player.ElementAt(i).getRole = 1;
                    
                    // เช็ค 2 เด้ง
                    if (player.ElementAt(i).typeCard[0] == player.ElementAt(i).typeCard[1] ||
                        player.ElementAt(i).scoreCard[0] == player.ElementAt(i).scoreCard[1])
                    {  //ถ้าดอกใบที่ และแต้ม ใบที่ 1 2 เหมือนกัน //active x2 
                        player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().enabled = true;
                        player.ElementAt(i).getRole = 2; //ส่งลำดับของไพ่
                    }

                    if (player.ElementAt(i).totalScore == 8 || player.ElementAt(i).totalScore == 9) //pok 8,9
                    {//if
                        player.ElementAt(i).ActiveAniamtion(cntCard);  //call ActiveAnimation funtion in Player.cs
                        player.ElementAt(i).score.GetComponent<MeshRenderer>().enabled = true;  //active score text 
                        player.ElementAt(i).bgscore.enabled = true;
                        player.ElementAt(i).X2X3.SetActive(true);
                        player.ElementAt(i).getText = 8; //ส่งค่า Text
                        player.ElementAt(i).getRole = 8; //ส่งลำดับของไพ่
                        if(player.ElementAt(i).totalScore == 9)
                            player.ElementAt(i).getRole = 9;

                }//end if
                    
               
            }//end loop
 
            host.getRole = 1;
            host.getText = 1;
            // เช็ค 2 เด้ง
            if (host.typeCard[0] == host.typeCard[1] ||
                host.scoreCard[0] == host.scoreCard[1]){  //ถ้าดอกใบที่ และแต้ม ใบที่ 1 2 เหมือนกัน //active x2 
                host.X2X3.GetComponent<SpriteRenderer>().enabled = true;
                host.getRole = 2; //ส่งลำดับของไพ่
            }

            if (host.totalScore == 8 || host.totalScore == 9) //auto open all player, when host get pok 8,9
            {//if else
                host.ActiveAniamtion(cntCard); //open host card
                host.bgscore.enabled = true;
                host.score.GetComponent<MeshRenderer>().enabled = true;  //active score text
                host.getText = 8;
                host.getRole = 8;
                if (host.totalScore == 9)
                    host.getRole = 9;
                for (int i = 0; i < 9; i++)
                {//loop
                    player.ElementAt(i).ActiveAniamtion(cntCard);  //call ActiveAnimation funtion in Player.cs (open all of player card)
                    player.ElementAt(i).score.GetComponent<MeshRenderer>().enabled = true;      //active score text
                    player.ElementAt(i).bgscore.enabled = true;
                    player.ElementAt(i).X2X3.SetActive(true);
                }//end loop

                PokdengUIManager.current.UItexttimer.enabled = false;
                PokdengUIManager.current.UIbgtimer.enabled = false;
                yield return new WaitForSeconds(1);
                PokdengUIManager.current.UItexttimer.enabled = true;
                PokdengUIManager.current.UIbgtimer.enabled = true;
                for (int i = 5; i > 0; i--)
                {
                    PokdengUIManager.current.UItexttimer.text = "" + i;
                    yield return new WaitForSeconds(1f);
                }
                drawCard = 0;
                PokdengUIManager.current.UItexttimer.enabled = false;
                PokdengUIManager.current.UIbgtimer.enabled = false;

            }//end if

            else
            {
                PokdengUIManager.current.UItexttimer.enabled = false;
                PokdengUIManager.current.UIbgtimer.enabled = false;
                yield return new WaitForSeconds(1);
                PokdengUIManager.current.UItexttimer.enabled = true;
                PokdengUIManager.current.UIbgtimer.enabled = true;
                for (int i = 5; i > 0; i--)
                {
                    PokdengUIManager.current.UItexttimer.text = "" + i;
                    yield return new WaitForSeconds(1f);
                }
                drawCard = 3;
                PokdengUIManager.current.UItexttimer.enabled = false;
                PokdengUIManager.current.UIbgtimer.enabled = false;
                PokdengUIManager.current.UnActiveBTDraworPass();
            }

        }//end cnt = 2
        else if (cntCard == 3)
        {//if cnt = 3
            //setting dealy 10 sec before draw card
            PokdengUIManager.current.UItexttimer.enabled = true;
            PokdengUIManager.current.UIbgtimer.enabled = true;
            if (player.ElementAt(4).requestCard == true)
            {
                OpenCard.current.activeCardAnimation2.SetActive(true);
                PokdengUIManager.current.ActiveBTDraworPass();
            }
            for (int i = 7; i >= 0; i--)
            {
                PokdengUIManager.current.UItexttimer.text = "" + i;
                yield return new WaitForSeconds(1f);
            } //UI timer text
            OpenCard.current.activeCardAnimation2.SetActive(false);
            
            player.ElementAt(4).ActiveAniamtion(cntCard);
            yield return new WaitForSeconds(2f);

            //Player data
            for (int i = 0; i < 9; i++)
            {//loop
                player.ElementAt(i).ActiveAniamtion(cntCard);  //active animation
                player.ElementAt(i).score.GetComponent<MeshRenderer>().enabled = true;  //active score text
                player.ElementAt(i).bgscore.enabled = true;
                if (player.ElementAt(i).cardPlayer[2].active)  //if active card3 equal true
                {
                    player.ElementAt(i).X2X3.SetActive(false); //X2X3 gameObject will be false (waiting status card3)
                    player.ElementAt(i).getText = 1;
                    player.ElementAt(i).getRole = 1;  
                }
                else if (!player.ElementAt(i).cardPlayer[2].active)
                {
                    player.ElementAt(i).typeCard[2] = 0;
                    player.ElementAt(i).scoreCard[2] = 0;
                    player.ElementAt(i).checkSort = "";
                }

                // เช็ค 3 เด้ง ไพ่ดอกเหมือนกัน
                if (player.ElementAt(i).typeCard[0] == player.ElementAt(i).typeCard[1] &&
                    player.ElementAt(i).typeCard[0] == player.ElementAt(i).typeCard[2] &&
                    player.ElementAt(i).typeCard[1] == player.ElementAt(i).typeCard[2]){ //if else
                    player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().enabled = true;
                    player.ElementAt(i).X2X3.SetActive(true);
                    player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[1];
                    player.ElementAt(i).getRole = 3;
                }//end if

                // เช็ค 3 เด้ง ไพ่เซียน
                if (player.ElementAt(i).scoreCard[0] > 0 && player.ElementAt(i).scoreCard[0] <= .9 &&
                    player.ElementAt(i).scoreCard[1] > 0 && player.ElementAt(i).scoreCard[1] <= .9 &&
                    player.ElementAt(i).scoreCard[2] > 0 && player.ElementAt(i).scoreCard[2] <= .9){
                    player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[1];
                    player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().enabled = true;
                    player.ElementAt(i).X2X3.SetActive(true);
                    player.ElementAt(i).getText = 6;
                    player.ElementAt(i).getRole = 4;
                }

                // เช็ค 3 เด้ง (ไพ่เรียง)
                for (int j = 0; j < 66; j++)
                {//loop
                    if (i != j) //ถ้าจำนวนของ player ไม่เท่ากับจำนวน ไพ่เรียง
                    {//if
                        if (player.ElementAt(i).checkSort == sorf[j]) //ถ้าไพ่ทั้ง 3 เป็นไพ่เรียง
                        {//if
                            player.ElementAt(i).X2X3.SetActive(true);
                            player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().enabled = true;
                            player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[1];
                            player.ElementAt(i).getText = 4;
                            player.ElementAt(i).getRole = 4.5f;

                            // เช็ค 5 เด้ง (ไพ่เรียง + ดอกเหมือนกัน) => สเตทฟรัช
                            if (player.ElementAt(i).typeCard[0] == player.ElementAt(i).typeCard[1] &&
                                player.ElementAt(i).typeCard[0] == player.ElementAt(i).typeCard[2] &&
                                player.ElementAt(i).typeCard[1] == player.ElementAt(i).typeCard[2]){ //if
                                player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[2]; //change image x2 x3 to x5
                                player.ElementAt(i).getText = 7;
                                player.ElementAt(i).getRole = 5;
                            }//end if
                        }//end if
                    }//end if                
                }//end loop
      
                // เช็ค 5 เด้ง (ไพ่ตอง)
                if (player.ElementAt(i).scoreCard[0] == player.ElementAt(i).scoreCard[1] &&
                    player.ElementAt(i).scoreCard[0] == player.ElementAt(i).scoreCard[2] &&
                    player.ElementAt(i).scoreCard[1] == player.ElementAt(i).scoreCard[2]){ //if else
                    player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().enabled = true;
                    player.ElementAt(i).X2X3.SetActive(true);
                    player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[2]; //change image x2 x3 to x5
                    player.ElementAt(i).getText = 5;
                    player.ElementAt(i).getRole = 5.5f;
                }//end if

                if (player.ElementAt(i).cardPlayer[2].active == false)
                    player.ElementAt(i).X2X3.SetActive(true);

                if (host.cardHost[2].active == false)
                    host.X2X3.SetActive(true);

            }//end loop

            //Host data
            host.ActiveAniamtion(cntCard); //call ActiveAnimation in host.cs
            host.score.GetComponent<MeshRenderer>().enabled = true;   //active score text 
            host.bgscore.enabled = true;
            if (host.cardHost[2].active) //ถ้า การ์ดใบที่ 3 เปิด ให้ทำงาน if นี้
            {
                host.X2X3.SetActive(false); //unactive x2x3 gameobject
                host.getText = 1;
                host.getRole = 1;

            }
            else if(!host.cardHost[2].active)
            {
                host.typeCard[2] = 0;
                host.scoreCard[2] = 0;
                host.checkSort = "";
            }

            if (host.typeCard[0] == host.typeCard[1] && host.typeCard[0] == host.typeCard[2] && host.typeCard[1] == host.typeCard[2]) 
            {//if เช็คดอกทั้ง 3 เหมือนกัน
                host.X2X3.SetActive(true);  //active x3
                host.X2X3.GetComponent<SpriteRenderer>().enabled = true;
                host.X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[1]; //change image x2 to x3
                host.getRole = 3;
            }//end if

            if (host.scoreCard[0] > 0 && host.scoreCard[0] <= .9 && host.scoreCard[1] > 0 && host.scoreCard[1] <= .9 && 
                host.scoreCard[2] > 0 && host.scoreCard[2] <= .9)
            {//if เช็คไพ่เซียน
                host.X2X3.SetActive(true);  //active x3
                host.X2X3.GetComponent<SpriteRenderer>().enabled = true;
                host.X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[1]; //change image x2 to x3
                host.getRole = 4;
                host.getText = 6;
            }//end if

            //เช็ค 3 ไพ่เรียง และ สเตทฟรัช
            for (int j = 0; j < 66; j++)
            {//loop
                if (host.checkSort == sorf[j]) //ถ้าไพ่ทั้ง 3 เป็นไพ่เรียง
                {//if
                    host.X2X3.SetActive(true);
                    host.X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[1];
                    host.X2X3.GetComponent<SpriteRenderer>().enabled = true;
                    host.getText = 4;
                    host.getRole = 4.5f;

                    // เช็ค 5 เด้ง (ไพ่เรียง + ดอกเหมือนกัน) => สเตทฟรัช
                    if (host.typeCard[0] == host.typeCard[1] && host.typeCard[0] == host.typeCard[2] && host.typeCard[1] == host.typeCard[2])
                    { //if
                        host.X2X3.SetActive(true);
                        host.X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[2]; //change image x2 x3 to x5
                        host.getText = 7;
                        host.getRole = 5;
                    }//end if
                }//end if              
            }//end loop

            //เช็ค 5 เด้ง (ไพ่ตอง)
            if (host.scoreCard[0] == host.scoreCard[1] &&host.scoreCard[0] == host.scoreCard[2] && host.scoreCard[1] == host.scoreCard[2])
            { //if else
                host.X2X3.SetActive(true);
                host.X2X3.GetComponent<SpriteRenderer>().sprite = X2X3Card[2]; //change image x2 x3 to x5
                host.X2X3.GetComponent<SpriteRenderer>().enabled = true;
                host.getText = 5;
                host.getRole = 5.5f;
            }//end if

            PokdengUIManager.current.UItexttimer.enabled = false;
            PokdengUIManager.current.UIbgtimer.enabled = false;
        } //end cnt = 3

        if (host.score.GetComponent<MeshRenderer>().enabled == true) //เมื่อคะแนนของเจ้าถูกเปิดออก
        {
            WinLose();
            StartCoroutine(NewGame());
        }

    }//end Calculate
    void WinLose() //active winner gameObject
    {
        for(int i=0;i<9;i++) //player 9 people
        {
            if (host.getRole >= 8 || player.ElementAt(i).getRole >= 8) //host get pok 8
            {
                if (host.getRole == player.ElementAt(i).getRole) //player get pok 8 too
                {
                    player.ElementAt(i).gameStatus = "Draw";  //this round is equal
                }
                else if (host.getRole < player.ElementAt(i).getRole) //player get pok 9
                {
                    player.ElementAt(i).gameStatus = "Win";  //player is win
                    player.ElementAt(i).Winner.enabled = true; //active winner gameobject
                }
                else
                {
                    player.ElementAt(i).gameStatus = "Lose"; //other condition player is lose
                }
            }

            //host or player get normal type card
            else if ((host.getRole >= 1 && host.getRole <= 3) && (player.ElementAt(i).getRole >= 1 && player.ElementAt(i).getRole <= 3))
            {

                if (host.totalScore == player.ElementAt(i).totalScore) //host and player is same point
                    player.ElementAt(i).gameStatus = "Draw";
                else if (host.totalScore < player.ElementAt(i).totalScore) //player get greater than host point
                {
                    player.ElementAt(i).gameStatus = "Win";
                    player.ElementAt(i).Winner.enabled = true;
                }
                else
                    player.ElementAt(i).gameStatus = "Lose"; //player get less then host point
            }

            //host or player get special of 3* card
            else if ((host.getRole >= 4 && host.getRole <= 4.5) || (player.ElementAt(i).getRole >= 1 && player.ElementAt(i).getRole <= 3))
            {
                if (host.getRole == player.ElementAt(i).getRole) //เจ้าและผู้เล่น ได้ไพ่เรียง หรือไพ่เซียนเหมือนกัน
                    player.ElementAt(i).gameStatus = "Draw";
                else if (host.getRole < player.ElementAt(i).getRole) //ผู้เล่นได้ไพ่เรียง เจ้าได้ไพ่เซียน
                {
                    player.ElementAt(i).gameStatus = "Win";
                    player.ElementAt(i).Winner.enabled = true;
                }
                else
                    player.ElementAt(i).gameStatus = "Lose"; //เจ้าได้ไพ่ลำดับที่สูงกว่า
            }

            //host or player get special of *5 card
            else
            {
                if (host.getRole == player.ElementAt(i).getRole) //เจ้าและผู้เล่น ได้ไพ่ตอง หรือสเตจฟรัทเหมือนกัน
                    player.ElementAt(i).gameStatus = "Draw";
                else if (host.getRole < player.ElementAt(i).getRole) //ผู้เล่นได้ไพ่ตอง เจ้าได้สเตจฟรัท
                {
                    player.ElementAt(i).gameStatus = "Win";
                    player.ElementAt(i).Winner.enabled = true;
                }
                else
                    player.ElementAt(i).gameStatus = "Lose"; 
            }
        } 
    }
    IEnumerator NewGame()
    {
        PokdengUIManager.current.UItexttimer.enabled = true;
        PokdengUIManager.current.UIbgtimer.enabled = true;
        for (int i = 10; i > 0; i--)
        {
            PokdengUIManager.current.UItexttimer.text = "" + i;
            yield return new WaitForSeconds(1f);
        }
        PokdengUIManager.current.UItexttimer.enabled = false;
        PokdengUIManager.current.UIbgtimer.enabled = false;

        Color defaultColor = new Color(255f, 255f, 255f, 255f); //default setting color
        for (int i = 0; i < 9; i++)//count of player
        {
            for (int j = 0; j < 3; j++) //count of card 1 2 3
            {
                player.ElementAt(i).cardPlayer[j].SetActive(false);
                player.ElementAt(i).bgcardPlayer[j].SetActive(false);
                player.ElementAt(i).bgcardPlayer[j].GetComponent<SpriteRenderer>().color = defaultColor;

                host.cardHost[j].SetActive(false);
                host.bgcardHost[j].GetComponent<SpriteRenderer>().color = defaultColor;
                host.bgcardHost[j].SetActive(false);
            }

            player.ElementAt(i).score.GetComponent<MeshRenderer>().enabled = false;
            player.ElementAt(i).bgscore.enabled = false;
            player.ElementAt(i).UnActiveAniamtion();
            player.ElementAt(i).X2X3.SetActive(false);
            player.ElementAt(i).X2X3.GetComponent<SpriteRenderer>().enabled = false;
            player.ElementAt(i).Winner.GetComponent<SpriteRenderer>().enabled = false;
            player.ElementAt(i).requestCard = false;
        }

       

        host.score.GetComponent<MeshRenderer>().enabled = false;
        host.bgscore.enabled = false;
        host.X2X3.SetActive(false);
        host.X2X3.GetComponent<SpriteRenderer>().enabled = false;
        host.UnActiveAniamtion();

         shuffledeck = true; //on function shuffledeck()  
    }

    


  

}
