using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Game Scene")]
    //public GameObject playerPrefab;
    //public GameObject PokdengGame;
    //public GameObject PokdengStart;
    //public GameObject HiroGame;


    public static GameManager toStatic;

    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;
    }
}
