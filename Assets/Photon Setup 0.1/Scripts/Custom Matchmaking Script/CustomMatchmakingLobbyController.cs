using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyConnectionButton;
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject mainPanel;

    public TMP_InputField playerNameInput;

    private string roomName;
    private int roomSize;
    
    private List<RoomInfo> roomListings;
    [SerializeField]
    private Transform roomContainer;
    [SerializeField]
    private GameObject roomListingPrefab;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        lobbyConnectionButton.SetActive(true);
        roomListings = new List<RoomInfo>();

        //check for player name saved to player prefs
        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName")=="")
            {
                PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName"); //get saved player name
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        }
        playerNameInput.text = PhotonNetwork.NickName;

    }
    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }
    public void JoinLobbyOnClick()
    {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if (roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomContainer.GetChild(tempIndex).gameObject);
            }
            if (room.PlayerCount > 0)
            {
                {
                    roomListings.Add(room);
                    ListRoom(room);
                }
            }
        }

    }
    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }
    void ListRoom (RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;

    }
    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }
    public void CreateRoom()
    {
        Debug.Log("Creating room now");
        RoomOptions roomOps = new RoomOptions(){IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom(roomName, roomOps );
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name ");
    }
    public void MatchMakingCancle()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}
