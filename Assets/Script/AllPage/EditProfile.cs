using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class EditProfile : MonoBehaviour
{
    [Header("Profile-Name")]
    public InputField playerNameInput;
    //public Text playerName;

    [Header("Profile-Image")]
    public GameObject SelectImagePanel;
    public Image playerImage;
    public List<Button> btImage = new List<Button>();

    public static EditProfile toStatic;

    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;
    }

    public void OnEditName()
    {
        string _playerName = playerNameInput.text;
        if (!string.IsNullOrEmpty(_playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = _playerName;
            PhotonNetwork.ConnectUsingSettings();
            
        }
        else
            Debug.Log("Playername is invalid!");
    }

    public void OnEditImageClicked()
    {
        SelectImagePanel.SetActive(true);
    }
    public void OnSelectImageClicked(Button bt)
    {
        playerImage.sprite = bt.GetComponent<Image>().sprite;
        SelectImagePanel.SetActive(false);
    }

    
}
