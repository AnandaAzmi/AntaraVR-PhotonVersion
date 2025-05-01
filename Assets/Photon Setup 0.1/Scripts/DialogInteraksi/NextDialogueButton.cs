using Photon.Pun;
using UnityEngine;

public class NextDialogueButton : MonoBehaviourPun
{
    public void OnClickNext()
    {
        if (!photonView.IsMine) return; // pastikan hanya Teacher yang bisa klik
        photonView.RPC("RPC_NextLine", RpcTarget.All);
    }

    [PunRPC]
    void RPC_NextLine()
    {
        DialogueManager.Instance.ShowNextLine(); // panggil ke semua klien
    }
}
