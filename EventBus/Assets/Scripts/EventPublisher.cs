using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventBus.TriggerEvent("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            EventBus.TriggerEvent("Launch");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            EventBus.TriggerEvent("Illuminate");
        }

        if (Input.GetKey(KeyCode.J))
        {
            EventBus.TriggerEvent("Jump");
        }

        if (Input.GetKey(KeyCode.B))
        {
            EventBus.TriggerEvent("Inflate");
        }
    }
}
