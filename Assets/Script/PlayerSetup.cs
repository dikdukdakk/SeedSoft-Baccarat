using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public TextMesh PlayerNameText;
    public SpriteRenderer PlayerImage;
    
    public void Initialize(int playerID,string playerName)
    {
        PlayerNameText.text = playerName;

        
    }
}
