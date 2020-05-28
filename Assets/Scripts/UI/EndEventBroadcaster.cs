using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndEventBroadcaster : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEndEvent;

    public void TriggerEnd()
    {
        OnEndEvent.Invoke();
    }
}
