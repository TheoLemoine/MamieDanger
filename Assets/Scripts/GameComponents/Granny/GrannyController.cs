using System.Collections;
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

        private float _randomThisSecond;
        
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int WalkSpeedHash = Animator.StringToHash("WalkSpeed");
        private static readonly int SayHelloHash = Animator.StringToHash("SayHello");
        private static readonly int WiggleHash = Animator.StringToHash("Wiggle");
        private static readonly int CelebrateHash = Animator.StringToHash("Celebrate");

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            StartCoroutine(UpdateRandom());
        }
        
        private IEnumerator UpdateRandom()
        {
            for (;;)
            {
                _randomThisSecond = Random.Range(0f, 1f);
                yield return new WaitForSeconds(1f);
            }
        }

        private void Update()
        {
            if (IsInteracting())
            {
                _agent.SetDestination(_followTransform.TransformPoint(offset));
            }

            animator.ResetTrigger(SayHelloHash);
            animator.ResetTrigger(WiggleHash);
            
            if (0.6 <= _randomThisSecond && _randomThisSecond <= 0.8)
                animator.SetTrigger(SayHelloHash);
            else if (0.8 <= _randomThisSecond && _randomThisSecond <= 1)
                animator.SetTrigger(WiggleHash);
            
            animator.SetFloat(SpeedHash, _agent.velocity.magnitude);
            animator.SetFloat(WalkSpeedHash, (_agent.velocity.magnitude + 1) * animationSpeedModifier);
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

        public void StartCelebrate()
        {
            animator.SetBool(CelebrateHash, true);
        }

        public void StopCelebrate()
        {
            animator.SetBool(CelebrateHash, false);
        }
    }
}