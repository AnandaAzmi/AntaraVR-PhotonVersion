using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SynkVRHead : MonoBehaviourPun
{
    public Transform dummyHead; // head bone / head dummy
    public Transform realVRHead; // vr camera dari scene 1 (hanya di student device)

    void Update()
    {
        if (photonView.IsMine && realVRHead != null)
        {
            dummyHead.position = realVRHead.position;
            dummyHead.rotation = realVRHead.rotation;
        }
    }
}
