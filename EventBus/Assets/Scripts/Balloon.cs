using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private bool m_IsQuitting;
    private bool m_IsInflated;

    public void OnEnable()
    {
        EventBus.StartListening("Inflate", Inflate);
    }

    public void OnApplicationQuit()
    {
        m_IsQuitting = true;
    }

    public void OnDisable()
    {
        if (m_IsQuitting == false)
        {
            EventBus.StopListening("Inflate", Inflate);
        }
    }

    void Inflate()
    {
        this.transform.localScale += new Vector3(.01f, .01f, .01f);
        Debug.Log("Received an inflate event : inflating balloon!");
    }

    public void Update()
    {
        if (this.transform.localScale.x > 3)
        {
            this.transform.position += new Vector3(0, .1f, 0);
        }
    }
}
