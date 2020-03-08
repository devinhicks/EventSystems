using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private bool m_IsQuitting;
    private bool m_IsJumping;

    public void OnEnable()
    {
        EventBus.StartListening("Jump", Jump);
    }

    public void OnApplicationQuit()
    {
        m_IsQuitting = true;
    }

    public void OnDisable()
    {
        if (m_IsQuitting == false)
        {
            EventBus.StopListening("Jump", Jump);
        }
    }

    void Jump()
    {
        if (!m_IsJumping)
        {
            m_IsJumping = true;
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, 10, 0), ForceMode.VelocityChange);
            Debug.Log("Received a jump event : frog jumping!");
        }
    }

    public void Update()
    {
        if (Physics.Raycast(this.transform.position, Vector3.down, 0.55f))
            {
                m_IsJumping = false;
            }

    }
}
