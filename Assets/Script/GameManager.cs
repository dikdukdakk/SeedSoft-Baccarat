using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;
    public GameObject[] postoInstance;

    public static GameManager instance;

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
        if (PhotonNetwork.IsConnected)
        {
            if(playerPrefab != null)
            {
                int randomPos = Random.Range(0,9);
                PhotonNetwork.Instantiate(playerPrefab.name, postoInstance[randomPos].transform.position, postoInstance[randomPos].transform.rotation);
            } 
        }
    }
	#endregion

    public override void OnLeftRoom()
	{
		SceneManager.LoadScene("01_GameLauncher");
	}

	public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
