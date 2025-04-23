using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazeInput : PointerInputModule
{
    public float ClickTime = 1f;

    private PointerEventData pointerData;
    private GameObject currentObject = null;
    private float gazeTime = 0;

    protected override void Awake()
    {
        base.Awake();
        pointerData = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        pointerData.Reset();
        pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2); // titik tengah layar

        eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
        pointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = pointerData.pointerCurrentRaycast.gameObject;
        m_RaycastResultCache.Clear();

        HandleGaze();
    }

    private void HandleGaze()
    {
        if (currentObject != null)
        {
            gazeTime += Time.deltaTime;

            if (gazeTime >= ClickTime)
            {
                ExecuteEvents.Execute(currentObject, pointerData, ExecuteEvents.pointerClickHandler);
                gazeTime = 0;
            }
        }
        else
        {
            gazeTime = 0;
        }
    }
}
