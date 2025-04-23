using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class VrHeadController : MonoBehaviour
{
    private Transform studentHeadDummy; // head bone atau objek kepala di prefab student

    void Start()
    {
        StartCoroutine(FindOwnedPlayerHead());
    }

    IEnumerator FindOwnedPlayerHead()
    {
        // Tunggu sampai prefab student muncul
        while (studentHeadDummy == null)
        {
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                PhotonView pv = player.GetComponent<PhotonView>();
                if (pv != null && pv.IsMine)
                {
                    // Misal head dummy berada dalam child bernama "Head"
                    studentHeadDummy = player.transform.Find("Head");
                    break;
                }
            }
            yield return null;
        }
    }

    void Update()
    {
        if (studentHeadDummy != null)
        {
            studentHeadDummy.position = transform.position;
            studentHeadDummy.rotation = transform.rotation;
        }
    }
}
