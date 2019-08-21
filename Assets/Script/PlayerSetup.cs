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
    public GameObject btStartGame;
    public SpriteRenderer isJoinRoom;

    [Header("Pokdeng GameObject")]
    public GameObject[] FrontCard;    //card player
    public GameObject[] BackgroundCard;  //background card
    public PhotonView photonView;

    private bool isPlayerJoinRoom = true;
    //public int firstCard,secondCard,thirdCard;


    #region Unity Method
    private void Start()
    {
       // SetPlayerUI();
    }
    #endregion

    #region Public Method   
    public void Initialize(int playerID, string playerName)
    {
        PlayerNameText.text = playerName;

        SetPlayerJoinRoom(isPlayerJoinRoom);
        ExitGames.Client.Photon.Hashtable initialProps = new ExitGames.Client.Photon.Hashtable() { { MultiplayPKGame.PLAYER_GREATERONE, isPlayerJoinRoom} };
        PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);

        //SetPlayerJoinRoom(isPlayerJoinRoom);
        //ExitGames.Client.Photon.Hashtable newProps = new ExitGames.Client.Photon.Hashtable() { { MultiplayPKGame.PLAYER_GREATERONE, isPlayerJoinRoom } };
        //PhotonNetwork.LocalPlayer.SetCustomProperties(newProps);


    }

    public void SetPlayerJoinRoom(bool playerJoin)
    {
        isJoinRoom.enabled = playerJoin;
    }
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    
    #endregion
}
