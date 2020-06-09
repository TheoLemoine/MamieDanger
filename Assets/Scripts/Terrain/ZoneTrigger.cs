using UnityEngine;
using UnityEngine.Events;
using Utils.Attributes;

namespace Terrain
{
    public class ZoneTrigger : MonoBehaviour
    {
        [SerializeField] [TagSelector] protected string targetTag;
        [SerializeField] protected UnityEvent triggerEvent;

        private void Start()
        {
            if (triggerEvent == null) triggerEvent = new UnityEvent();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
                triggerEvent.Invoke();
            
        }
    }
}
