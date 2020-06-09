using UnityEngine;

public class OnceZoneTrigger : ZoneTrigger
{
    private bool _hasBeenTriggered;

    private void OnTriggerEnter(Collider other)
    {    
        if (!_hasBeenTriggered && other.CompareTag(targetTag))
        {
            triggerEvent.Invoke();
            _hasBeenTriggered = true;
        }
    }
}
