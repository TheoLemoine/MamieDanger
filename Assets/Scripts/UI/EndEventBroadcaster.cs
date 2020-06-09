using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class EndEventBroadcaster : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnEndEvent;

        public void TriggerEnd()
        {
            OnEndEvent.Invoke();
        }
    }
}
