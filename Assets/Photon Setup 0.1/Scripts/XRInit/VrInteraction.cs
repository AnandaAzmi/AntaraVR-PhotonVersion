using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VrInteraction : MonoBehaviour
{
    public float waktuLihat = 2f; // waktu tunggu sebelum klik
    private float timer;
    private GameObject objekYangDilihatSebelumnya;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject target = hit.collider.gameObject;

            if (target == objekYangDilihatSebelumnya)
            {
                timer += Time.deltaTime;
                if (timer >= waktuLihat)
                {
                    ExecutePointerDown(target);
                    timer = 0f;
                }
            }
            else
            {
                objekYangDilihatSebelumnya = target;
                timer = 0f;
            }
        }
        else
        {
            objekYangDilihatSebelumnya = null;
            timer = 0f;
        }
    }

    void ExecutePointerDown(GameObject target)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerDownHandler);
    }
}
