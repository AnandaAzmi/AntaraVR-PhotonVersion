using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using TMPro;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiPlayerSceneIndex;

    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject roomPanel;

    [SerializeField]
    public GameObject startButton;

    [SerializeField]
    private GameObject teacherButton;  // Tombol Pilih Teacher
    [SerializeField]
    private GameObject studentButton;  // Tombol Pilih Student

    [SerializeField]
    private Transform playersContainer;
    [SerializeField]
    private GameObject playerListingPrefab;

    [SerializeField]
    private TextMeshProUGUI roomNameDisplay;


    void ClearPlayerListings()
    {
        for (int i = playersContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(playersContainer.GetChild(i).gameObject);
        }
    }

    void ListPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playersContainer);
            TextMeshProUGUI tempText = tempListing.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tempText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            EnableRoleSelectionButtons(true);  // Aktifkan tombol role selection
        }
        else
        {
            startButton.SetActive(false);
            EnableRoleSelectionButtons(false);  // Nonaktifkan tombol role selection
        }
        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }

    }
    // Fungsi untuk mengaktifkan dan menonaktifkan tombol pilihan role
    public void EnableRoleSelectionButtons(bool isEnabled)
    {
        teacherButton.SetActive(isEnabled);  // Menampilkan tombol Teacher
        studentButton.SetActive(isEnabled);  // Menampilkan tombol Student
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Ambil role MasterClient
            string masterRole = PhotonNetwork.LocalPlayer.CustomProperties["role"]?.ToString();

            if (string.IsNullOrEmpty(masterRole))
            {
                Debug.LogError("MasterClient belum memilih role.");
                return;
            }

            // Tentukan role lawan (otomatis untuk client lain)
            string otherRole = masterRole == "Teacher" ? "PhotonPlayer" : "Teacher";

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();

                if (player == PhotonNetwork.LocalPlayer) // MasterClient
                {
                    playerProps["role"] = masterRole;
                }
                else // Player lainnya
                {
                    playerProps["role"] = otherRole;
                }

                player.SetCustomProperties(playerProps);
            }

            // Tutup room dan mulai game
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(multiPlayerSceneIndex);
        }
    }

    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick()
    {
        lobbyPanel.SetActive(true );
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine( rejoinLobby());
    }

}
