using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool m_IsQuitting;
    GameObject cannonBall;

    public void OnEnable()
    {
        EventBus.StartListening("Shoot", Shoot);
    }

    public void OnApplicationQuit()
    {
        m_IsQuitting = true;
    }

    public void OnDisable()
    {
        if (m_IsQuitting == false)
        {
            EventBus.StopListening("Shoot", Shoot);
        }
    }

    void Shoot()
    {
        // Load and fire cannonball in direction cannon is facing
        cannonBall = Resources.Load<GameObject>("cannonBall");
        Instantiate(cannonBall, this.transform.position, this.transform.rotation);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddExplosionForce(100f, this.transform.forward, 50f);

        Debug.Log("Received a shoot event : shooting cannon!");
    }
}
