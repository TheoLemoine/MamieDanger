using UnityEngine;
using UnityEngine.Events;
using Utils.Attributes;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] [TagSelector] private string tag;
    [SerializeField] private UnityEvent triggerEvent;

    private void Start()
    {
        if (triggerEvent == null) triggerEvent = new UnityEvent();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
            triggerEvent.Invoke();
            
    }
}
