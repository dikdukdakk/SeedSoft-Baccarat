using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMesh textName;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            
        }
        else
        {

        }

        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if(textName != null)
            textName.text = photonView.Owner.NickName;
    }
}
