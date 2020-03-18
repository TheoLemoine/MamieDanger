using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private LayerMask clickableLayer;
        [SerializeField] private float maxRaycastDistance = 100f;
        
        private Camera _cam;
        private NavMeshAgent _agent;
        private Transform _transform;
        
        private Dictionary<int, IInteractable> _onRangeObjects = new Dictionary<int, IInteractable>();
        private bool _isInteracting;
        
        private void Start()
        {
            _cam = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _transform = GetComponent<Transform>();
        }

        public void AddInRange(int gameObjectId, IInteractable interactable)
        {
            _onRangeObjects.Add(gameObjectId, interactable);
        }

        public void RemoveFromRange(int gameObjectId)
        {
            _onRangeObjects.Remove(gameObjectId);
        }

        public bool IsInteracting()
        {
            return _isInteracting;
        }

        public void OnTap(Vector2 cursor)
        {
            var ray = _cam.ScreenPointToRay(cursor);

            if (Physics.Raycast(ray, out var hit, maxRaycastDistance, clickableLayer))
            {
                var id = hit.collider.gameObject.GetInstanceID();
                
                if (_onRangeObjects.TryGetValue(id, out var interactable))
                {
                    // prevent from moving
                    _agent.SetDestination(_transform.position);

                    if (interactable.IsInteracting())
                    {
                        interactable.InteractStop();
                        _isInteracting = false;
                    }
                    else
                    {
                        interactable.InteractStart(gameObject);
                        _isInteracting = true;
                    }
                }
            }
        }
    }
}
