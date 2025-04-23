using System.Collections;
using System.Collections.Generic;
using Google.XR.Cardboard;
using UnityEngine;

public class CardboardStartup2 : MonoBehaviour
{
    private bool xrReady = false;

    public void ActivateCardboard()
    {
        xrReady = true;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }
    }
    void Update()
    {
        if (!xrReady) return;

        if (Api.IsGearButtonPressed)
        {
            Api.ScanDeviceParams();
        }

        if (Api.IsCloseButtonPressed)
        {
            Application.Quit();
        }

        if (Api.IsTriggerHeldPressed)
        {
            Api.Recenter();
        }

        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }

        Api.UpdateScreenParams();
    }
}
