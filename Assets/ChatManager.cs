using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPun,IPunObservable
{
    public PhotonView photonView;
    public GameObject bubbleSpeech;
    public TextMesh chatText;

    public GameObject btsendChat;
    InputField chatInput;
    private bool DisableSend;

    private void Awake()
    {
        chatInput = GameObject.Find("Chat-InputField").GetComponent<InputField>();
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            if(!DisableSend && chatInput.isFocused)
            {
                if (chatInput.text != "")
                {
                    btsendChat.SetActive(true);
                }
                else
                {
                    btsendChat.SetActive(false);
                }
            }
        }
    }

    [PunRPC]
    void SendMassage(string massage)
    {
        chatText.text = massage;
        StartCoroutine(hideBubbleSpeech());
    }
    IEnumerator hideBubbleSpeech()
    {
        yield return new WaitForSeconds(3);
        bubbleSpeech.SetActive(false);
        chatText.text = "";
        DisableSend = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(bubbleSpeech.activeSelf);
        }
        else if (stream.IsReading)
        {
            bubbleSpeech.SetActive((bool)stream.ReceiveNext());
        }
    }

    public void OnSendChatClicked(string data)
    {
        photonView.RPC("SendMassage", RpcTarget.AllBuffered, chatInput.text);
        bubbleSpeech.SetActive(true);
        chatInput.text = "";
        DisableSend = true;
        btsendChat.SetActive(false);
    }
}
