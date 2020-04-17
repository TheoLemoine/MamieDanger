using UnityEngine;
using UnityEngine.AI;
using Global.Input;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer groundLayer;
        private NavMeshAgent _agent;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _agent = GetComponent<NavMeshAgent>();
            ListenInputs();
        }
        
        private void Move(RaycastHit hit)
        {
            _agent.SetDestination(hit.point);
        }

        public void IgnoreInputs(bool interruptRoute = false)
        {
            InputManager.PlayerRaycaster.RemoveListener(groundLayer.LayerIndex, Move);
            if (interruptRoute) _agent.SetDestination(_transform.position);
        }

        public void ListenInputs()
        {
            InputManager.PlayerRaycaster.AddListener(groundLayer.LayerIndex, Move);
        }
    }
}
