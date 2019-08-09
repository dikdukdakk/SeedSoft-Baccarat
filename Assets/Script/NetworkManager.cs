using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Firsttime Login")]
    public GameObject EnterGamePanel;
    public InputField playerNameInput;

    [Header("Connection Panel & Lobby Panel")]
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;

    [Header("Create Room UI Panel")]
    public GameObject CreateRoom;
    public InputField roomNameInputField;
    public InputField roomPasswordInputField;

    [Header("List Room UI Panel")]
    public GameObject ListRoom;
    public GameObject roomListEntryPrefab;
    public GameObject roomListParent;
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListGameObject; 


    #region Unity Methods
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    { 
        ActivatePanel(EnterGamePanel.name);

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObject = new Dictionary<string, GameObject>();
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName +  " is Connected to photon sever.");
        ActivatePanel(ConnectionStatusPanel.name);
        ActivatePanel(LobbyPanel.name);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internect");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        //CreateAndJoinRoom();
    }

    #endregion

    #region Private methods
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " join to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("02_Pokdeng"); //Load pokdeng scene
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }


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
    public void ActivatePanel(string panelActivated)
    {
        EnterGamePanel.SetActive(panelActivated.Equals(EnterGamePanel.name));
        ConnectionStatusPanel.SetActive(panelActivated.Equals(ConnectionStatusPanel.name));
        LobbyPanel.SetActive(panelActivated.Equals(LobbyPanel.name));
        CreateRoom.SetActive(panelActivated.Equals(CreateRoom.name));
        
    }

    public void ConnectToPhotonSever()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ActivatePanel(ConnectionStatusPanel.name);
            ActivatePanel(EnterGamePanel.name);
        }
    }

   //public void JoinRandomRoom()
   // {
   //     PhotonNetwork.JoinRandomRoom();
   // }

    #endregion

    #region UI Button Callbacks
    public void OnEnterFirstNameClicked()
    {
        string playerName = playerNameInput.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
            Debug.Log("Playername is invalid!");
    }

    public void OnEnterCreateRoomClicked()
    {
        string roomName = roomNameInputField.text;
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room" + Random.Range(0, 10000);
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 9; //set count of play in room
        roomOptions.IsOpen = true;    //on off room
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnEnterCancelCreateRoomClicked()
    {
        ActivatePanel(CreateRoom.name);
        ActivatePanel(LobbyPanel.name);
    }

    public void OnShowRoom()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            
        }
        ListRoom.SetActive(true) ;
        //ActivatePanel(LobbyPanel.name);
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
                if(cachedRoomList.ContainsKey(room.Name)) //update cachedRoom list
                {
                    cachedRoomList[room.Name] = room;
                }
                else
                {
                    cachedRoomList.Add(room.Name, room); // add the new room to the cached room
                    
                }
                
            }
            
        }

        foreach(RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomListEntry = Instantiate(roomListEntryPrefab);
            roomListEntry.transform.SetParent(roomListParent.transform);
            roomListEntry.transform.localScale = Vector3.one;

            roomListEntry.transform.Find("RoomNameText").GetComponent<Text>().text = room.Name;
            //roomListParent.transform.Find("Text-Number").GetComponent<Text>().text = room.;
            roomListEntry.transform.Find("BT-JoinRoom").GetComponent<Button>().onClick.AddListener(() => OnJoinRoomButtonClicked(room.Name));

            roomListGameObject.Add(room.Name, roomListEntry);

        }
    }

    #endregion

}
