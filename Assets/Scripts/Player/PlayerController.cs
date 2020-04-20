using System;
using UnityEngine;
using UnityEngine.AI;
using Global.Input;
using UnityEngine.Events;
using Utils;

namespace Player
{
    [Serializable]
    class ChangeDestinationEvent : UnityEvent<Vector3, Vector3>
    {}
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer groundLayer;
        [SerializeField] private ChangeDestinationEvent changeDestinationEvent;
        private NavMeshAgent _agent;
        private Transform _transform;

        private void Start()
        {
            if (changeDestinationEvent == null)
                changeDestinationEvent = new ChangeDestinationEvent();
            
            _transform = transform;
            _agent = GetComponent<NavMeshAgent>();
            ListenInputs();
        }

        private void OnDestroy()
        {
            IgnoreInputs();
        }

        private void Move(RaycastHit hit)
        {
            changeDestinationEvent.Invoke(hit.point, hit.normal);
            _agent.SetDestination(hit.point);
        }

        public void IgnoreInputs(bool interruptRoute = false)
        {
            if (InputManager.IsReady)
            {
                InputManager.PlayerRaycaster.RemoveListener(groundLayer.LayerIndex, Move);
            }
            if (interruptRoute) _agent.SetDestination(_transform.position);
        }

        public void ListenInputs()
        {
            if (InputManager.IsReady)
            {
                InputManager.PlayerRaycaster.AddListener(groundLayer.LayerIndex, Move);
            }
        }
    }
}
