using UnityEngine;
using UnityEngine.AI;
using Abstract;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteractor : AInteractorBehaviour
    {
        [SerializeField] private LayerMask clickableLayer;
        [SerializeField] private float maxRaycastDistance = 100f;
        
        private Camera _cam;
        private NavMeshAgent _agent;
        private Transform _transform;

        private Vector2 _pointer;

        private void Start()
        {
            _cam = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _transform = GetComponent<Transform>();
        }
        
        public void UpdatePointer(InputAction.CallbackContext value)
        {
            _pointer = value.ReadValue<Vector2>();
        }
        
        public void Interact()
        {
            var ray = _cam.ScreenPointToRay(_pointer);

            if (Physics.Raycast(ray, out var hit, maxRaycastDistance, clickableLayer))
            {
                var id = hit.collider.gameObject.GetInstanceID();
                
                if (_interactablesInRange.TryGetValue(id, out var interactable))
                {
                    // prevent from moving
                    _agent.SetDestination(_transform.position);
                    interactable.Interact(this);
                }
            }
        }
    }
}
