using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventSequencer : MonoBehaviour
{
    [Serializable]
    private struct Event {
        public float time;
        public UnityEvent unityEvent;
    }
    [SerializeField] private Event[] events;
    private Event[] _processedEvents;
    void Start()
    {
        float time = 0f;
        _processedEvents = new Event[events.Length];
        for (int i = 0; i < events.Length; i++)
        {
            var currentEvent = events[i];
            _processedEvents[i] = new Event()
            {
                time = currentEvent.time - time,
                unityEvent = currentEvent.unityEvent
            };
            time = currentEvent.time;
        }
    }

    public void StartSequence()
    {
        StartCoroutine(DOSequence());
    }

    private IEnumerator DOSequence()
    {
        foreach (var processedEvent in _processedEvents)
        {
            yield return new WaitForSeconds(processedEvent.time);
            processedEvent.unityEvent.Invoke();
        }
    }
}
