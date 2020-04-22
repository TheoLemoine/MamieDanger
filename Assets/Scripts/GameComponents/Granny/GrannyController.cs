using Abstract;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GameComponents.Granny
{
    public class GrannyController : AInteracToggleBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float slowFactor = 1.5f;
        [SerializeField] private Animator animator;
        [SerializeField] private float animationSpeedModifier = 1;
        
        private NavMeshAgent _agent;
        private Transform _followTransform;
        private NavMeshAgent _followAgent;
        
        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
        private static readonly int WalkSpeedHash = Animator.StringToHash("WalkSpeed");

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

            var isWalking = _agent.velocity.magnitude > .01f;
            
            animator.SetBool(IsWalkingHash, isWalking);
            animator.SetFloat(WalkSpeedHash, isWalking ? _agent.velocity.magnitude * animationSpeedModifier : 1f);
        }

        protected override void InteractStart(IInteractor interactor)
        {
            var interactorGO = interactor.GetGameObject();
            _followTransform = interactorGO.GetComponent<Transform>();
            _followAgent = interactorGO.GetComponent<NavMeshAgent>();
            _followAgent.speed /= slowFactor;
        }

        protected override void InteractStop()
        {
            _followAgent.speed *= slowFactor;
            
            _followTransform = null;
            _followAgent = null;
        }
    }
}