using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float maxRaycastDistance = 100f;

        private NavMeshAgent _agent;
        private Camera _cam;

        private Vector2 _pointer;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _cam = Camera.main;
        }

        public void UpdatePointer(InputAction.CallbackContext value)
        {
            _pointer = value.ReadValue<Vector2>();
        }
        
        public void Move()
        {
            var ray = _cam.ScreenPointToRay(_pointer);

            if (Physics.Raycast(ray, out var hit, maxRaycastDistance, groundLayer))
            {
                _agent.SetDestination(hit.point);
            }
        }

    }
}
