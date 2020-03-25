using UnityEngine;
using UnityEngine.AI;
using Abstract;

namespace Player
{
    public class PlayerInteractor : AInteractorBehaviour
    {
        [SerializeField] private LayerMask clickableLayer;
        [SerializeField] private float maxRaycastDistance = 100f;
        
        private Camera _cam;
        private NavMeshAgent _agent;
        private Transform _transform;

        private void Start()
        {
            _cam = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _transform = GetComponent<Transform>();
        }
        
        public void OnTap(Vector2 cursor)
        {
            var ray = _cam.ScreenPointToRay(cursor);

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
