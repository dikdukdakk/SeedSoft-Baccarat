using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PKManager : MonoBehaviourPunCallbacks
{
    [Header("Player & Host Position")]
    public GameObject playerPrefab;
    public GameObject[] playerPosition;
    public GameObject hostPosition;
    bool playerHost;
    bool playerLogin;
    
    [Header("Host & Player")]
    public GameObject host;
    public List<GameObject> player = new List<GameObject>();

    public static PKManager instance;

    #region Unity methods
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        playerHost = NetworkManager.toStatic.playertoHost;
        //playerPrefab = GameManager.toStatic.playerPrefab;

        if (PhotonNetwork.IsConnected)
        {
            if (GameManager.toStatic.playerPrefab != null)
            {
                //set host position
                if (playerHost)
                {
                    host = PhotonNetwork.Instantiate(GameManager.toStatic.playerPrefab.name, hostPosition.transform.position, hostPosition.transform.rotation);
                    host.gameObject.tag = "Host";
                }
                //set player position 
                else
                {
                    PhotonNetwork.Instantiate(GameManager.toStatic.playerPrefab.name, playerPosition[0].transform.position, playerPosition[0].transform.rotation);
                    
                }  
            }

        }

        
    }

   

    private void Update()
    {
        
    }
    #endregion





}
