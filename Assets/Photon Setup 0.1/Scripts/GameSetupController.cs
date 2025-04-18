using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    // Callback saat pemain baru masuk ke room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} telah bergabung!");
    }

    // Callback saat pemain keluar
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} telah meninggalkan room.");
    }

}
