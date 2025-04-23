using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VrInteraction : MonoBehaviour
{
    public float waktuLiahat = 2f;
    private float waktu;
    private bool lihatKe;
    public GameObject buttonPlay;
    private void Update()
    {
        if (lihatKe)
        {
            waktu += Time.deltaTime;
            if (waktu >= waktuLiahat)
            {
                ExecuteEvents.Execute(buttonPlay, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                waktu = 0;
            }
        }
    }
    public void PointerEnter()
    {
        lihatKe = true;
    }
    public void PointerExit()
    {
        lihatKe = false;
    }
}
