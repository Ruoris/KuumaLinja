using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerListing _playerListing;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomsCanvases _roomsCanvases;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private void Start()
    {
        GetCurrentRoomPlayers();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        // SetReadyUp(false);
        GetCurrentRoomPlayers();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++)
        {
            Destroy(_listings[i].gameObject);
           
        } 
        _listings.Clear();
    }
    private void SetReadyUp(bool state)
    {
        //_ready = state;
        //if (ready)
        //{
        //    _readyUpText.text = "R";

        //}
        //else
        //{
        //    _readyUpText.text = "N";
        //}
    }
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }




    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;

        }
        if (PhotonNetwork.CurrentRoom == null||PhotonNetwork.CurrentRoom.Players==null)
        {
            return;
        }
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);

        }

    }


    private void AddPlayerListing(Player player)
    {

        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, _content);

            if (listing != null)
            {
                Debug.Log("Instanitated: " + listing);
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }

        }

    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {


        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    public void Onclick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(11);
        }
        
    }

}
