using UnityEngine;

[CreateAssetMenu(fileName = "New Module", menuName = "Module System/Module Data")]
public class ModuleData : ScriptableObject
{
    public int moduleId;
    public string moduleName;
    public GameObject modulePrefab;
}
