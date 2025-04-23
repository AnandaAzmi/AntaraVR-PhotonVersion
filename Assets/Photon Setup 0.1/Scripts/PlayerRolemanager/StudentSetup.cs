using System.Collections;
using Photon.Pun;
using UnityEngine;

public class StudentSetup : MonoBehaviour
{
    [SerializeField] private GameObject vrSetup; // Assign this in the Inspector
    [SerializeField] private PhotonView photonView;

    private void Start()
    {
        StartCoroutine(SetupVRDelayed());
    }

    IEnumerator SetupVRDelayed()
    {
        yield return new WaitForSeconds(0.1f); // Delay sejenak agar PhotonView siap

        // Mengaktifkan vrSetup tanpa pengecekan pemilik
        Debug.Log("Mengaktifkan VR Setup untuk semua pemain (tanpa pengecekan IsMine).");
        vrSetup.SetActive(true);
    }

    //IEnumerator SetupVRDelayed()
    //{
    //    yield return new WaitForSeconds(0.1f); // Delay sejenak agar PhotonView siap

    //    if (photonView.IsMine)
    //    {
    //        Debug.Log("IsMine TRUE: Mengaktifkan VR Setup.");
    //        vrSetup.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.Log("IsMine FALSE: Menonaktifkan VR Setup.");
    //        vrSetup.SetActive(false);
    //    }
    //}
}
