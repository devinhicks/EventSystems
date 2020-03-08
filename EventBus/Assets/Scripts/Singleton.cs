using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // instance accessible only be the getter
    private static T m_Instance;
    public static bool m_isQutting;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                // making sure that there's not other instances
                // of the same type in memory
                m_Instance = FindObjectOfType<T>();

                if (m_Instance == null)
                {
                    // Making sure that there's not other instances
                    // of the same type in memory
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    m_Instance = obj.AddComponent<T>();
                }
            }
            return m_Instance;
        }
    }

    // virtual awake() that can be overridden in a derived class
    public virtual void Awake()
    {
        if (m_Instance == null)
        {
            // if null, this instance is now the singleton instance
            // of assigned type
            m_Instance = this as T;

            // Making sure that my singleton instance
            // will persist in mmory across every scene
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // destory current onstance because it must be a duplicate
            Destroy(gameObject);
        }
    }
}
