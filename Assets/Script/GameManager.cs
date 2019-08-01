using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public int countPlayer;

    void Awake()
    {
        current = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        RegisterPlayerPokdeng(countPlayer);
    }

    public static void RegisterPlayerPokdeng(int countPlayer)
    {
        if (countPlayer == 0)
            return;
    }

    
}
