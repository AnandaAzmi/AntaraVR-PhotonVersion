using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAliveVR : MonoBehaviour
{
    public GameObject vrHeadObject; // Objek VR Head (di device student)
    public VrHeadController vrHeadControllerScript; // Script VRHeadController
    private static KeepAliveVR instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Pastikan di scene 1 script VRHeadController dimatikan
        if (vrHeadControllerScript != null)
        {
            vrHeadControllerScript.enabled = false;
        }
    }

    public void SwitchToMultiplayer()
    {
        // Aktifkan script VRHeadController saat di scene multiplayer
        if (vrHeadControllerScript != null)
        {
            vrHeadControllerScript.enabled = true;
        }
    }
}
