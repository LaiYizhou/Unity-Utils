using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static readonly object syncObject = new object();
    public static T Instance
    {
        get
        {
            lock (syncObject)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }
            }
            return instance;
        }
    }
}