using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerSetup : MonoBehaviourPun, IPunObservable
{
    public PhotonView photonView;

    [Header("UI References")]
    public TextMesh PlayerNameText;
    public SpriteRenderer PlayerImage;

    [Header("Pokdeng GameObject")]
    public GameObject[] FrontCard;    //card player
    public GameObject[] BackgroundCard;  //background card
    public int firstCard,secondCard,thirdCard;

   
    #region Unity Method
  
    private void Start()
    {
        SetPlayerUI();
    }

    private void Update()
    {
        photonView.RPC("GetCard", RpcTarget.AllBuffered, PKManager.toStatic.isGameStatus);
    }
    #endregion

    #region Private Method
    [PunRPC]
    void GetCard(bool gameStatus)
    {
        if (!gameStatus)
            return;

        FrontCard[0].SetActive(true);
        FrontCard[1].SetActive(true);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            firstCard = ShuffleDeck.toStatic.firstCard[photonView.Owner.ActorNumber]; //+8
            secondCard = ShuffleDeck.toStatic.secondCard[photonView.Owner.ActorNumber];
        }
        else
        {
            firstCard = ShuffleDeck.toStatic.firstCard[photonView.Owner.ActorNumber];
            secondCard = ShuffleDeck.toStatic.secondCard[photonView.Owner.ActorNumber];
        }
        FrontCard[0].GetComponent<SpriteRenderer>().sprite = ShuffleDeck.toStatic.card[firstCard];
        FrontCard[1].GetComponent<SpriteRenderer>().sprite = ShuffleDeck.toStatic.card[secondCard];

    }
    #endregion

  
    #region Event method
    public enum EventCodes
    {
        STARTGAME = 0 
    }
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    
    //byte event code, object content, int senderID
    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        object content = photonEvent.CustomData;

        //EventCodes
    }

    private void StartGame()
    {
        object[] datas = new object[] { photonView.ViewID };
    }
    #endregion


    #region Private Method
    void SetPlayerUI()
    {
        if (PlayerNameText != null)
            PlayerNameText.text = photonView.Owner.NickName;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
//        throw new System.NotImplementedException();
    }
    #endregion
}
