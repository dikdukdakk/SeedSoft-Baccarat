using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;

public class PKManager : MonoBehaviourPunCallbacks
{
    public static PKManager toStatic;

    [Header("Player & Host Position")]
    public int countPlayer;
    public GameObject playerPrefab;
    public GameObject hostPosition;
    public List<GameObject> playerPosition;
    private Dictionary<int, GameObject> playerListGameObjects;

    public bool isShuffledeck;

    [Header("UI Pokdeng")]
    public GameObject btstartgame;
    public GameObject InputChat;

    public bool isGameStatus;
    public enum RaiseEventsCode
    {
        STARTGAME_EVENTCODE = 0
    }

    #region Unity methods
    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;
    }

    private void Start()
    {
        isShuffledeck = true;
        countPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            {
                if (countPlayer == 1)
                    PhotonNetwork.Instantiate(playerPrefab.name, hostPosition.transform.position, hostPosition.transform.rotation);
                else
                    PhotonNetwork.Instantiate(playerPrefab.name, playerPosition[countPlayer - 2].transform.position, playerPosition[countPlayer - 2].transform.rotation);
            }

            if (playerListGameObjects == null)
                playerListGameObjects = new Dictionary<int, GameObject>();

            //foreach(Photon.Realtime.Player _player in PhotonNetwork.PlayerList)
            //{
            //    GameObject playerListGameObject = playerPrefab;
            //      playerListGameObjects.Add(_player.ActorNumber, playerListGameObject);
            //}

        }
    }
    /*
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RaiseEventsCode.STARTGAME_EVENTCODE)
        {
            object[] data = (object[])photonEvent.CustomData;
            string nickNameOfPlayer = (string)data[0];
            isGameStatus = true;
        }
    }
    */



    private void Update()
    {
        ActiveButtonStart();

        GameStart(isGameStatus);
    }
    #endregion

    #region Photon Callbacks
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        //Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
        //playerListGameObjects.Remove(otherPlayer.ActorNumber);

    }

    public override void OnLeftRoom()
    {
        //foreach (GameObject playerlistGameObject in playerListGameObjects.Values)
        //    Destroy(playerlistGameObject);

        //playerListGameObjects.Clear();
        //playerListGameObjects = null;

        PhotonNetwork.LoadLevel("01_GameLauncher");
        //if (PhotonNetwork.LocalPlayer.IsMasterClient)
    }
    #endregion

    #region Private Method
    void ActiveButtonStart()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
            btstartgame.SetActive(true);
        else
            btstartgame.SetActive(false);
    }

    void GameStart(bool gameStatus)
    {//GameStatus
        if (!gameStatus)  //ถ้าค่าที่รับเข้ามาเป็น false ให้ออก , เกมจบแล้วให้ออก
            return;
        
        ShuffleDeck.toStatic.OnShuffleDeck(isShuffledeck); //สลับการ์ด
        /*
        string nickName = photonView.Owner.NickName;
        //event data
        object[] data = new object[] { nickName,gameStatus };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
        };

        //send option
        SendOptions sendOptions = new SendOptions
        {
            Reliability = false
        };

        PhotonNetwork.RaiseEvent((byte)RaiseEventsCode.STARTGAME_EVENTCODE, data,raiseEventOptions, sendOptions);
        */

    }//end GameStatus
    
    #endregion



    #region Public method

    #endregion

    #region UI Button Callbacks
    public void OnLeaveRoomButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnStartGameButtonClicked()
    {
        isGameStatus = true;
    }

    public void OnChatButtonClicked()
    {
        InputChat.SetActive(true);
    }
    #endregion




}
