using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour
{
    private bool m_IsQuitting;
    private bool m_IsIlluminated;

    public void OnEnable()
    {
        EventBus.StartListening("Illuminate", Illuminate);
    }

    public void OnApplicationQuit()
    {
        m_IsQuitting = true;
    }

    public void OnDisable()
    {
        if (m_IsQuitting == false)
        {
            EventBus.StopListening("Illuminate", Illuminate);
        }
    }

    void Illuminate()
    {
        if (!m_IsIlluminated)
        {
            m_IsIlluminated = true;
            this.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log("Received an illuminate event : illuminating the bulb!");
        }

        else
        {
            m_IsIlluminated = false;
            this.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Received an illuminate event : de-illuminating the bulb!");
        }
    }
}
