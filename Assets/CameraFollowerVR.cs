using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerVR : MonoBehaviour
{
    [Header("VR Camera yang akan diikuti")]
    public Transform vrCamera;

    void Update()
    {
        if (vrCamera != null)
        {
            // Ikuti rotasi VR (tanpa posisi)
            transform.rotation = vrCamera.rotation;
        }
    }

    // Auto-link VR Camera saat start
    void Start()
    {
        if (vrCamera == null)
        {
            // Coba cari kamera dengan tag "MainCamera" dari scene sebelumnya
            GameObject foundCam = GameObject.FindGameObjectWithTag("MainCamera");

            if (foundCam != null)
            {
                vrCamera = foundCam.transform;
                Debug.Log("VR Camera ditemukan dan ditautkan otomatis.");
            }
            else
            {
                Debug.LogWarning("VR Camera tidak ditemukan! Pastikan kamera VR dari Scene 1 ada dan diberi tag 'MainCamera'");
            }
        }
    }
}
