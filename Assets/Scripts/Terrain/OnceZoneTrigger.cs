using UnityEngine;

public class OnceZoneTrigger : ZoneTrigger
{
    private bool _hasTrigger;

    protected new void OnTriggerEnter(Collider other)
    {
        if (!_hasTrigger)
        {
            base.OnTriggerEnter(other);
            _hasTrigger = true;
        }
    }
}
