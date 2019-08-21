using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject SettingPanel;
    public GameObject ConnectPanel;
    public GameObject LobbyPanel;
    public GameObject CreateRoomPanel;
    public GameObject JoinRoomPanel;

    [Header("Game References")]
    public GameObject PKGamePanel;
    public GameObject HRGamePanel;

    public static UIManager toStatic;

    #region Unity Methods
    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;
    }

    private void Start()
    {
        ActivatePanel(SettingPanel.name);
    }
    #endregion

    #region Public Medthods
    public void ActivatePanel(string panelActivated)
    {
        SettingPanel.SetActive(panelActivated.Equals(SettingPanel.name));
        ConnectPanel.SetActive(panelActivated.Equals(ConnectPanel.name));
        LobbyPanel.SetActive(panelActivated.Equals(LobbyPanel.name));
        CreateRoomPanel.SetActive(panelActivated.Equals(CreateRoomPanel.name));
        PKGamePanel.SetActive(panelActivated.Equals(PKGamePanel.name));
        //HRGamePanel.SetActive(panelActivated.Equals(HRGamePanel.name));

        if (LobbyPanel.active)
            NetworkManager.toStatic.OnShowRoom();
    }
    #endregion
}