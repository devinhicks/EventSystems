using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private bool m_IsQuitting;
    private bool m_IsLaunched = false;

    public void OnEnable()
    {
        EventBus.StartListening("Launch", Launch);
    }

    public void OnApplicationQuit()
    {
        m_IsQuitting = true;
    }

    public void OnDisable()
    {
        if (m_IsQuitting == false)
        {
            EventBus.StopListening("Launch", Launch);
        }
    }

    void Launch()
    {
        if (m_IsLaunched == false)
        {
            m_IsLaunched = true;
            Debug.Log("Received a launch event : rocket launching!");
        }
    }

    public void Update()
    {
        if (m_IsLaunched)
        {
            this.transform.position += new Vector3(0, .1f, 0);
        }

        if (this.transform.position.y > 20f)
        {
            Destroy(this);
        }
    }
}
