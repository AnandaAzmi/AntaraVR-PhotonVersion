using UnityEngine;
using Photon.Pun;

public class ModuleInstanceHandler : MonoBehaviour
{
    public static ModuleInstanceHandler Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SpawnModule(ModuleData moduleData, Vector3 position)
    {
        Instantiate(moduleData.modulePrefab, position, Quaternion.identity);
    }
    // Versi baru, return GameObject-nya
    public GameObject SpawnAndReturn(ModuleData moduleData, Vector3 position)
    {
        return Instantiate(moduleData.modulePrefab, position, Quaternion.identity);
    }
}
