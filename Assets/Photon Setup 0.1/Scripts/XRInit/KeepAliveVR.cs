using UnityEngine;

public class KeepAliveVR : MonoBehaviour
{
    private static KeepAliveVR instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
