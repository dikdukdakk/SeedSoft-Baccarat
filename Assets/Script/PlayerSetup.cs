using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPun,IPunObservable
{
    [Header("UI References")]
    public TextMesh PlayerNameText;
    public SpriteRenderer PlayerImage;

    [Header("Pokdeng GameObject")]
    public int rollCard;
    public GameObject[] FrontCard;    //card player
    public GameObject[] BackgroundCard;  //background card
    public PhotonView photonView;


    public int firstCard,secondCard,thirdCard;

   
    #region Unity Method
  
    private void Start()
    {
        SetPlayerUI();
    }

    private void Update()
    {
        //GetCard(PKManager.toStatic.isGameStatus);

        photonView.RPC("GetCard", RpcTarget.AllBuffered,PKManager.toStatic.isGameStatus);
    }
    #endregion

    #region Private Method
    [PunRPC]
    void GetCard(bool gameStatus)
    {
        if (!gameStatus)
            return;

        if(photonView.IsMine)
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                //firstCard = ShuffleDeck.toStatic.firstCard[photonView.Owner.ActorNumber +8];
                //secondCard = ShuffleDeck.toStatic.secondCard[photonView.Owner.ActorNumber +8];
                //Debug.Log("(Host) firstCard = " + firstCard + " secondCard = " + secondCard);

                BackgroundCard[0].SetActive(true);
                BackgroundCard[1].SetActive(true);
            }
        }
        else
        {
            //firstCard = ShuffleDeck.toStatic.firstCard[photonView.Owner.ActorNumber  -2];
            //secondCard = ShuffleDeck.toStatic.secondCard[photonView.Owner.ActorNumber -2];
            //Debug.Log("(Player" + PKManager.toStatic.countPlayer + ") firstCard = " + firstCard + " secondCard = " + secondCard);

            BackgroundCard[0].SetActive(true);
            BackgroundCard[1].SetActive(true);
        }
    

    }

    void SetPlayerUI()
    {
        if (PlayerNameText != null)
            PlayerNameText.text = photonView.Owner.NickName;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
    #endregion
}
