using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
   

    public RoomInfo RoomInfo { get;  private set;  }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        //_text = transform.GetChild(0).GetComponent<TextMeshPro>();
        RoomInfo = roomInfo;
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
        
    }
        
    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
