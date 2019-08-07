using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;

    #region Unity Methods
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    #endregion


    #region Public Methods
    public void ConnectToPhotonSever()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }  
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
   
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName +  " is Connected to photon sever.");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

     
    public override void OnConnected()
    {
        Debug.Log("Connected to Internect");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    #endregion

    #region Provate methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 5000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;    //on off room
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20; //set count of play in room

        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);

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
    #endregion
}
