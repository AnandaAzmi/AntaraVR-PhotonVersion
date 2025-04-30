using Photon.Pun;
using UnityEngine;

public class ModuleSpawner : MonoBehaviourPun
{
    [SerializeField] private ModuleDatabase moduleDatabase;
    [SerializeField] private Transform spawnPoint;
   
    private void Awake()
    {
       
        if (spawnPoint == null)
        {
            GameObject spawnObj = GameObject.FindWithTag("ModuleInstance");
            if (spawnObj != null)
            {
                spawnPoint = spawnObj.transform;
            }
            else
            {
                Debug.LogError("ModuleSpawn with tag 'ModuleInstance' not found!");
            }
        }

        if (moduleDatabase == null)
        {
            moduleDatabase = FindObjectOfType<ModuleDatabase>();
            if (moduleDatabase == null)
            {
                Debug.LogError("ModuleDatabase not found in scene!");
            }
        }
    }
    // Fungsi ini dipanggil saat teacher klik tombol spawn
    public void SpawnModuleButton(int moduleId)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("Hanya Teacher (Master Client) yang bisa spawn module.");
            return;
        }

        photonView.RPC(nameof(RPC_SpawnModule), RpcTarget.AllBuffered, moduleId, spawnPoint.position);
    }
    public void EndModuleButton()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("Hanya Teacher (Master Client) yang bisa mengakhiri module.");
            return;
        }

        photonView.RPC(nameof(RPC_EndModule), RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_SpawnModule(int moduleId, Vector3 position)
    {
        var moduleData = moduleDatabase.GetModuleById(moduleId);
        if (moduleData != null)
        {
            ModuleInstanceHandler.Instance.SpawnModule(moduleData, position);
        }
    }

    [PunRPC]
    void RPC_EndModule()
    {
        if (ModuleInstanceHandler.Instance != null)
        {
            ModuleInstanceHandler.Instance.EndModule();
        }
    }
}
