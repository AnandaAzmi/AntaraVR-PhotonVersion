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
        //Debug.Log("Creating Player");
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("role", out object roleObj))
        {
            string role = roleObj.ToString();
            string prefabName = role;

            Debug.Log($"Spawning player dengan role: {role}");

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabName), Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Role belum diset di CustomProperties!");
        }
        
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
