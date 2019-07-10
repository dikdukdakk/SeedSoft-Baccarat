using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int countPlayer;

    void Awake()
    {
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

    public void PlayPokdengGame()
    {
        countPlayer++;
        SceneManager.LoadScene("PokdengGame");
    }
}
