using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerRoleManager : MonoBehaviour
{
    
    public void ChooseRole(string role)
    {
        
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("Hanya MasterClient yang boleh memilih role.");
            return; // Kalau bukan, keluar dari fungsi
        }

        
        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();
        playerProps["role"] = role; // Simpan role yang dipilih

        
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);
        
        Debug.Log($"Role dipilih oleh MasterClient: {role}");

    }

}
