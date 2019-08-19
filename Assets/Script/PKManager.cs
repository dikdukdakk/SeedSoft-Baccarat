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
    //private Dictionary<int, GameObject> playerListGameObjects;

    public bool isShuffledeck;

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

          
        }
    }
  



    private void Update()
    {
        ActiveButtonStart();

        GameStart(isGameStatus);
    }
    #endregion

    #region Photon Callbacks
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
       

    }

	public override void OnLeftRoom()
	{

		PhotonNetwork.LoadLevel("01_GameLauncher");
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
