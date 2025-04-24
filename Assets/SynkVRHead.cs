using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SynkVRHead : MonoBehaviourPun, IPunObservable
{
    [Header("Referensi Kamera VR (opsional, bisa otomatis)")]
    public Transform vrCamera;

    private Quaternion networkRotation;
    private PhotonView photonViewStudent;

    void Awake()
    {
        photonViewStudent = GetComponent<PhotonView>();
        networkRotation = transform.rotation;
    }

    void Start()
    {
        // Kalau ini milik kita dan belum ada vrCamera, coba cari otomatis
        if (photonViewStudent.IsMine && vrCamera == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("MainCamera");
            if (found != null)
            {
                vrCamera = found.transform;
                Debug.Log("VR Camera ditemukan otomatis dan ditautkan.");
            }
            else
            {
                Debug.LogWarning("VR Camera tidak ditemukan! Pastikan kamera VR diberi tag 'MainCamera'.");
            }
        }
    }

    void Update()
    {
        if (photonViewStudent.IsMine)
        {
            // Hanya pemilik yang mengikuti VR
            if (vrCamera != null)
            {
                transform.rotation = vrCamera.rotation;
            }
        }
        else
        {
            // Di sisi lain, gunakan rotasi dari jaringan
            transform.rotation = Quaternion.Slerp(transform.rotation, networkRotation, Time.deltaTime * 10f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Kirim rotasi (dari device student / VR aktif)
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Terima rotasi (di device teacher)
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
