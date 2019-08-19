using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EditLobby : MonoBehaviour
{
    [Header("Profile Player")]
    public Text textPlayer;
    public Image imgPlayer;

    public static EditLobby toStatic;

    private void Awake()
    {
        if (toStatic != null)
            Destroy(this.gameObject);
        else
            toStatic = this;
    }

    private void Update()
    {
        textPlayer.text = PhotonNetwork.LocalPlayer.NickName;
        imgPlayer.sprite = EditProfile.toStatic.playerImage.sprite;
    }
}
