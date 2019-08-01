using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIPokdeng; //popup pokdeng game
    //public GameObject UIHiro;

    //Main Menu UI
    public void ActvePokdengUI()
    {
        UIPokdeng.SetActive(true);
    }
    public void PlayPokdengGame()
    {
        GameManager.current.countPlayer++;
        SceneManager.LoadScene("03_PokdengGame");
    }

    //Login Menu UI
    public void LoginTravelingUser()
    {
        SceneManager.LoadScene("02_MainMenu");
    }
   /* public void LoginFacebook()
    {

    }*/
}
