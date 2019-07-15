using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIPokdeng;

    public void ActvePokdengUI()
    {
        UIPokdeng.SetActive(true);
    }

    public void PlayPokdengGame()
    {
        GameManager.current.countPlayer++;
        SceneManager.LoadScene("PokdengGame");
    }
}
