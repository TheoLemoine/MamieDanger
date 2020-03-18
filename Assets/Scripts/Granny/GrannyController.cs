using System;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Granny
{
    public class GrannyController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float slowFactor = 1.5f;
        
        private NavMeshAgent _agent;
        private Transform _followTransform;
        private NavMeshAgent _followAgent;

        private bool _isFollowing;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_isFollowing)
            {
                _agent.SetDestination(_followTransform.TransformPoint(offset));
            }
        }

        public void InteractStart(GameObject interactor)
        {
            _followTransform = interactor.GetComponent<Transform>();
            _followAgent = interactor.GetComponent<NavMeshAgent>();
            _isFollowing = true;

            _followAgent.speed /= slowFactor;
        }

        public void InteractStop()
        {
            _followAgent.speed *= slowFactor;
            
            _followTransform = null;
            _followAgent = null;
            _isFollowing = false;
        }

        public bool IsInteracting()
        {
            return _isFollowing;
        }
    }
}