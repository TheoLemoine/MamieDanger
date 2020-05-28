using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAfterTime : MonoBehaviour
{
    
    [SerializeField] private UnityEvent eventAfterTime;
    [SerializeField] private float time;
    
    void Start()
    {
        StartCoroutine(WaitForEvent());
    }

    private IEnumerator WaitForEvent()
    {
        yield return new WaitForSeconds(time);
        eventAfterTime.Invoke();
    }
}
