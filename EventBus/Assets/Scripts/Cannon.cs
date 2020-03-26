using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool m_IsQuitting;
    GameObject cannonBall;

    public float firingVelocity = 2500f;

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
        cannonBall = (GameObject)Instantiate(Resources.Load("cannonBall"),
            this.transform.position, this.transform.rotation);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.velocity = (this.transform.forward * firingVelocity * Time.deltaTime);

        Debug.Log(rb.velocity);
        Debug.Log("Received a shoot event : shooting cannon!");
    }
}
