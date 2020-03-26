using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    private Dictionary<string, UnityEvent> m_EventDictionary;

    // Establish Event Queue
    private int eventsInQueue = 0;
    private float dequeueDelay = 1f; // time to dequeue and trigger event
    private List<string> eventQueue;
    bool queueRunning = false;
    
    public override void Awake()
    {
        base.Awake();
        Instance.Init();
    }

    private void Init()
    {
        if (Instance.m_EventDictionary == null)
        {
            Instance.m_EventDictionary = new Dictionary<string, UnityEvent>();
        }
        if (Instance.eventQueue == null)
        {
            Instance.eventQueue = new List<string>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.m_EventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.m_EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void AddToQueue(string eventName)
    {
        Instance.eventQueue.Add(eventName);
        Debug.Log(eventName + " has been added to queue.");
        Instance.eventsInQueue++;
    }

    public IEnumerator DequeueEvent()
    {
        yield return new WaitForSeconds(dequeueDelay);
        while (eventsInQueue > 0)
        {
            Debug.Log(eventsInQueue + " events in queue");

            string currentEvent = eventQueue[0];
            Debug.Log("Event Dequeued: " + currentEvent);

            Instance.eventQueue.RemoveAt(0);
            Instance.eventsInQueue--;
            TriggerEvent(currentEvent);
            yield return new WaitForSeconds(dequeueDelay);
        }
        queueRunning = false;
    }

    public void Update()
    {
        if (eventsInQueue > 0 && queueRunning == false)
        {
            StartCoroutine("DequeueEvent");
            queueRunning = true;
        }
    }
}
