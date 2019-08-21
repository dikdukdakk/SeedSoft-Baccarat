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
    public Dictionary<int, GameObject> playerListGameObjects;

    public bool isShuffledeck;
    GameObject playerGameObject;

    [Header("UI Pokdeng")]
    public GameObject btstartgame;
    public GameObject InputChat;

    public bool isGameStatus;
   

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
        //isShuffledeck = true;
        countPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
        if (PhotonNetwork.IsConnected)
        {

        }
    }

    private void Update()
    {
        
    }

    #endregion

    #region Photon Callbacks

    #endregion

    #region Private Method
    void GameStart(bool gameStatus)
    {//GameStatus
        if (!gameStatus)  //ถ้าค่าที่รับเข้ามาเป็น false ให้ออก , เกมจบแล้วให้ออก
            return;
        
        //ShuffleDeck.toStatic.OnShuffleDeck(isShuffledeck); //สลับการ์ด     

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

    public void OnChatToggleClicked(bool toggle)
    {
        if (!toggle)
        {
            InputChat.GetComponent<Image>().enabled = true;
            InputChat.GetComponent<InputField>().enabled = true;
            InputChat.GetComponentInChildren<Text>().enabled = true;

        }
        else
        {
            InputChat.GetComponent<Image>().enabled = false;
            InputChat.GetComponent<InputField>().enabled = false;
            InputChat.GetComponentInChildren<Text>().enabled = false;
            InputChat.GetComponent<InputField>().text = "";
            GameObject.Find("Player(Clone)").GetComponent<ChatManager>().btsendChat.SetActive(false);

        }
    }
    #endregion




}
