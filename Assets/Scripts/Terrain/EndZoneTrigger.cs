using UnityEngine;
using UnityEngine.Events;
using Utils.Attributes;

public class EndZoneTrigger : MonoBehaviour
{
    [SerializeField] [TagSelector] private string grannyTag;
    [SerializeField] private UnityEvent triggerEvent;

    private void Start()
    {
        if (triggerEvent == null) triggerEvent = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(grannyTag))
            triggerEvent.Invoke();
            
    }
}
