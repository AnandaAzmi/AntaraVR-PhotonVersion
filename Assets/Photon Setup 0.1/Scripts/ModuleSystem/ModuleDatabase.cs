
using UnityEngine;

[CreateAssetMenu(fileName = "Module Database", menuName = "Module System/Module Database")]
public class ModuleDatabase : ScriptableObject
{
    public ModuleData[] modules;

    public ModuleData GetModuleById(int id)
    {
        foreach (var module in modules)
        {
            if (module.moduleId == id)
                return module;
        }
        Debug.LogWarning("Module ID not found: " + id);
        return null;
    }
}
