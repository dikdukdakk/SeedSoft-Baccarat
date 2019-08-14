using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PKManager : MonoBehaviourPunCallbacks
{
    bool playerHost;
    public bool playerEnterRoom;
    int playerCount;

    [Header("Host & Player")]
    public List<GameObject> playerSit = new List<GameObject>();
    public GameObject host;
    public List<GameObject> player = new List<GameObject>();

   // private Dictionary<int, GameObject> playerlistGameObject;

   
    public static PKManager toStatic;

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
        //playerPrefab = GameManager.toStatic.playerPrefab;
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;


        if (PhotonNetwork.IsConnected)
        {
            
            //if (playerPrefab != null)
            /*{
                //set host position
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    host = PhotonNetwork.Instantiate(playerPrefab.name, hostPosition.transform.position, hostPosition.transform.rotation);
                    host.gameObject.tag = "Host";
                }
                //set player position 
                else
                {
                    player.Add(PhotonNetwork.Instantiate(playerPrefab.name, playerPosition[playerCount - 2].transform.position,
                    playerPosition[playerCount-2].transform.rotation));  
                }

               
                playerEnterRoom = true;
                */
           // }

           // if(PhotonNetwork.LocalPlayer.)
            
        }

        
    }

    


    #endregion



   


}
