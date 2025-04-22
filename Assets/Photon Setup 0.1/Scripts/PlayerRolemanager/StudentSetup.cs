using Photon.Pun;
using UnityEngine;

public class StudentSetup : MonoBehaviour
{
    [SerializeField] private GameObject vrSetup; // Assign this in the Inspector
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Hanya aktifkan VR jika ini milik local player
        if (photonView.IsMine)
        {
            vrSetup.SetActive(true);
        }
        else
        {
            vrSetup.SetActive(false);
        }
    }
}
