using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class XRInitializer : MonoBehaviour
{
    private CardboardStartup2 cardboardStartup;

    private bool xrStarted = false;
    void Start()
    {
        cardboardStartup = FindObjectOfType<CardboardStartup2>();
        StartCoroutine(StartXR());
    }

    IEnumerator StartXR()
    {
        if (xrStarted)
        {
            Debug.LogWarning("XR already started. Skipping.");
            yield break;
        }
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        xrStarted = true;
        Debug.Log("XR Started.");
        // Baru sekarang aman panggil CardboardStartup
        if (cardboardStartup != null)
        {
            cardboardStartup.ActivateCardboard();
        }
        else
        {
            Debug.LogWarning("CardboardStartup not found in scene.");
        }
    }
}
