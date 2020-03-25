using UnityEngine;
using UnityEngine.AI;
using Abstract;
using Interfaces;

namespace Granny
{
    public class GrannyController : AInteracToggleBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float slowFactor = 1.5f;
        
        private NavMeshAgent _agent;
        private Transform _followTransform;
        private NavMeshAgent _followAgent;


        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (IsInteracting())
            {
                _agent.SetDestination(_followTransform.TransformPoint(offset));
            }
        }

        protected override void InteractStart(IInteractor interactor)
        {
            var interactorGO = interactor.GetGameObject();
            _followTransform = interactorGO.GetComponent<Transform>();
            _followAgent = interactorGO.GetComponent<NavMeshAgent>();
            _followAgent.speed /= slowFactor;
        }

        protected override void InteractStop(IInteractor interactor)
        {
            _followAgent.speed *= slowFactor;
            
            _followTransform = null;
            _followAgent = null;
        }
    }
}