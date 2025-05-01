using UnityEngine;
using Photon.Pun;

public class ModuleInstanceHandler : MonoBehaviour
{
    public static ModuleInstanceHandler Instance;
    private GameObject currentModuleInstance;
    private GameObject waitingArea;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        // Cari WaitingArea berdasarkan tag
        GameObject waitingObj = GameObject.FindGameObjectWithTag("WaitingArea");
        if (waitingObj != null)
        {
            waitingArea = waitingObj;
        }
        else
        {
            Debug.LogWarning("WaitingArea dengan tag 'WaitingArea' tidak ditemukan!");
        }
        // Cari module aktif yang sudah ada di scene berdasarkan tag
        GameObject existingModule = GameObject.FindGameObjectWithTag("ActiveModule");
        if (existingModule != null)
        {
            currentModuleInstance = existingModule;
        }
    }

    public void SpawnModule(ModuleData moduleData, Vector3 position)
    {
        // Hancurkan module sebelumnya jika ada
        if (currentModuleInstance != null)
        {
            Destroy(currentModuleInstance);
        }
        // Hancurkan waiting area jika ada
        if (waitingArea != null)
        {
            waitingArea.SetActive(false);
        }
        currentModuleInstance = Instantiate(moduleData.modulePrefab, position, Quaternion.identity);
    }

    public void EndModule()
    {
        // Aktifkan kembali WaitingArea jika masih ada referensi
        if (waitingArea != null)
        {
            waitingArea.SetActive(true);
        }
        else
        {
            // Cari lagi kalau null
            GameObject waitingObj = GameObject.FindGameObjectWithTag("WaitingArea");
            if (waitingObj != null)
            {
                waitingArea = waitingObj;
                waitingArea.SetActive(true);
            }
        }
        // Hapus semua object yang bertag "ActiveModule"
        GameObject[] activeModules = GameObject.FindGameObjectsWithTag("ActiveModule");
        foreach (GameObject module in activeModules)
        {
            Destroy(module);
        }

        currentModuleInstance = null;
    }
    
}
