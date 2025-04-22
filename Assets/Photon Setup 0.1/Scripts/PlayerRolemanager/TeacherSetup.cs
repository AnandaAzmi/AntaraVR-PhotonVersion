using Photon.Pun;
using UnityEngine;

public class TeacherSetup : MonoBehaviourPun
{
    private PhotonView myPhotonView;
    [SerializeField] private GameObject teacherUI;

    void Start()
    {
        myPhotonView = GetComponentInParent<PhotonView>();
        if (myPhotonView != null && myPhotonView.IsMine)
        {
            teacherUI.SetActive(true);
        }
        else
        {
            teacherUI.SetActive(false);
        }
    }
}
