using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

        private NavMeshAgent _agent;
        private Camera _cam;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float maxRaycastDistance = 100f;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _cam = Camera.main;
        }

        public void OnTap(InputAction.CallbackContext value)
        {
            var cursor = value.ReadValue<Vector2>();
            var ray = _cam.ScreenPointToRay(cursor);

            if (Physics.Raycast(ray, out var hit, maxRaycastDistance, groundLayer))
            {
                _agent.SetDestination(hit.point);
            }
        }

    }
}
