﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager toStatic;

    [Header("Firsttime Login")]
    //public GameObject EnterGamePanel;
    public InputField playerNameInput;
    public Text playerName;

    //[Header("Connection Panel & Lobby Panel")]
    //ublic GameObject ConnectionStatusPanel;
    //public GameObject LobbyPanel;


    [Header("Create Room UI Panel")]
    //public GameObject CreateRoom;
    public InputField roomNameInputField;
    public InputField roomPasswordInputField;


    //[Header("Create Random Join Room UI Panel")]
    //public GameObject CreateJoinRandomRoom;


    [Header("List Room UI Panel")]
    public GameObject ListRoom;
    public GameObject roomListEntryPrefab;
    public GameObject roomListParent;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListGameObject;

    private Dictionary<int, GameObject> playerListGameObjects;

   


    #region Unity Methods
    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObject = new Dictionary<string, GameObject>();
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " is Connected to photon sever.");
        //ActivatePanel(ConnectionStatusPanel.name);
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.LobbyPanel.name);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internect");
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.ConnectPanel.name);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " join to " + PhotonNetwork.CurrentRoom.Name);
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.PKGamePanel.name);

        if (playerListGameObjects == null)
            playerListGameObjects = new Dictionary<int, GameObject>();

        foreach (Photon.Realtime.Player _player in PhotonNetwork.PlayerList)
        {
            GameObject player = Instantiate(PKManager.toStatic.playerPrefab);
            PhotonView photonView = player.GetComponent<PhotonView>();

            if (PhotonNetwork.AllocateViewID(photonView))
            {
                object[] data = new object[]
                {
                    player.transform.position, player.transform.rotation, photonView.ViewID
                };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others,
                    CachingOption = EventCaching.AddToRoomCache
                };

                SendOptions sendOptions = new SendOptions
                {
                    Reliability = true
                };

                //PhotonNetwork.RaiseEvent(custom, data, raiseEventOptions, sendOptions);

                player.GetComponent<PlayerSetup>().Initialize(_player.ActorNumber, _player.NickName);
                playerListGameObjects.Add(_player.ActorNumber, player);
            }

            
        }

    }



    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " is member of " + PhotonNetwork.CurrentRoom.PlayerCount);
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.PKGamePanel.name);

        GameObject player = Instantiate(PKManager.toStatic.playerPrefab);
        PhotonView photonView = player.GetComponent<PhotonView>();

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[]
            {
                player.transform.position, player.transform.rotation, photonView.ViewID
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            //PhotonNetwork.RaiseEvent(CustomManualInstantiationEventCode, data, raiseEventOptions, sendOptions);

        }

        player.GetComponent<PlayerSetup>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);
        playerListGameObjects.Add(newPlayer.ActorNumber, player);
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //GameObject playerListGameObject;
        //if (playerListGameObjects.TryGetValue(target.ActorNumber, out playerListGameObject))
        //{
        //   object isPlayerJoinRoom;
         //   if (changedProps.TryGetValue(MultiplayPKGame.PLAYER_GREATERONE, out isPlayerJoinRoom))
        //    {
        //        playerListGameObject.GetComponent<PlayerSetup>().SetPlayerJoinRoom((bool)isPlayerJoinRoom);
        //    }
        //}
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
        playerListGameObjects.Remove(otherPlayer.ActorNumber);
        
    }

    public override void OnLeftRoom()
    {
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.LobbyPanel.name);
        foreach (GameObject playerListGameObject in playerListGameObjects.Values)
            Destroy(playerListGameObject);

        playerListGameObjects.Clear();
        playerListGameObjects = null;
    }

    public override void OnLeftLobby()
    {
        ClearRoomListView();
        cachedRoomList.Clear();
    }
    #endregion

    #region Private methods
    void OnJoinRoomButtonClicked(string _roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        PhotonNetwork.JoinRoom(_roomName);
    }

    void ClearRoomListView()
    {
        foreach (var roomListGameObject in roomListGameObject.Values)
        {
            Destroy(roomListGameObject);
        }

        roomListGameObject.Clear();
    }
    #endregion

    #region Public Methods
    public void ConnectToPhotonSever()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            UIManager.toStatic.ActivatePanel(UIManager.toStatic.ConnectPanel.name);
            UIManager.toStatic.ActivatePanel(UIManager.toStatic.SettingPanel.name);
        }
    }
    #endregion

    #region UI Button Callbacks
    public void OnEnterCreateRoomClicked()
    {
        string roomName = roomNameInputField.text;
        if (roomNameInputField.text == "")
        {
            int randomName = Random.Range(1, 5);
            switch (randomName)
            {
                case 1: roomName = "มาเล่นกันเถอะ!!"; break;
                case 2: roomName = "ป็อกกกรอบวงไปสิ"; break;
                case 3: roomName = "อยากเสียเงินจัง<3"; break;
                case 4: roomName = "เก่งกว่านี้มีไหม"; break;
            }
        }

        roomName = "" + Random.Range(0, 100) + " " + roomName;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10; //set count of play in room
        roomOptions.IsOpen = true;    //on off room
        roomOptions.IsVisible = true;



        PhotonNetwork.CreateRoom(roomName, roomOptions);

    }

    public void OnEnterCancelCreateRoomClicked()
    {
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.CreateRoomPanel.name);
        UIManager.toStatic.ActivatePanel(UIManager.toStatic.LobbyPanel.name); 
    }

    public void OnLeaveRoomButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnShowRoom()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();

        }
        ListRoom.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);

            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(room.Name))
                    cachedRoomList.Remove(room.Name);
            }
            else
            {
                if (cachedRoomList.ContainsKey(room.Name)) //update cachedRoom list      
                    cachedRoomList[room.Name] = room;
                else
                    cachedRoomList.Add(room.Name, room); // add the new room to the cached room

            }

        }

        foreach (RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomListEntry = Instantiate(roomListEntryPrefab);
            roomListEntry.transform.SetParent(roomListParent.transform);

            roomListEntry.transform.Find("Text-Name").GetComponent<Text>().text = room.Name;
            roomListEntry.transform.Find("Text-CountPlayer").GetComponent<Text>().text = room.PlayerCount + " / " + room.MaxPlayers;
            roomListEntry.transform.Find("BT-JoinRoom").GetComponent<Button>().onClick.AddListener(() => OnJoinRoomButtonClicked(room.Name));

            roomListGameObject.Add(room.Name, roomListEntry);

        }
    }

   

    #endregion

}

