using System;
using System.Collections;
using Global.Input;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace GameComponents.Player
{
    [Serializable]
    class ChangeDestinationEvent : UnityEvent<Vector3, RaycastHit>
    {}
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer groundLayer;
        [SerializeField] private ChangeDestinationEvent changeDestinationEvent;
        [SerializeField] private Animator animator;
        
        private NavMeshAgent _agent;
        private Transform _transform;

        private float _randomThisSecond;
        
        private static readonly int ScratchHeadHash = Animator.StringToHash("ScratchHead");
        private static readonly int LookAroundHash = Animator.StringToHash("LookAround");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        private void Start()
        {
            if (changeDestinationEvent == null)
                changeDestinationEvent = new ChangeDestinationEvent();
            
            _transform = transform;
            _agent = GetComponent<NavMeshAgent>();
            ListenInputs();

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
            animator.ResetTrigger(ScratchHeadHash);
            animator.ResetTrigger(LookAroundHash);
            
            if (0.6 <= _randomThisSecond && _randomThisSecond <= 0.8)
                animator.SetTrigger(ScratchHeadHash);
            else if (0.8 <= _randomThisSecond && _randomThisSecond <= 1)
                animator.SetTrigger(LookAroundHash);
            
            animator.SetFloat(SpeedHash, _agent.velocity.magnitude);
        }

        private void OnDestroy()
        {
            IgnoreInputs();
        }

        private void Move(RaycastHit hit)
        {
            _agent.SetDestination(hit.point);
            changeDestinationEvent.Invoke(_agent.destination, hit);
        }

        public void IgnoreInputs(bool interruptRoute = false)
        {
            if (InputManager.IsReady)
                InputManager.PlayerRaycaster.RemoveListener(groundLayer.LayerIndex, Move);

            if (interruptRoute) InterruptRoute();
        }

        public void InterruptRoute()
        {
            _agent.SetDestination(_transform.position);
        }

        public void ListenInputs()
        {
            if (InputManager.IsReady)
                InputManager.PlayerRaycaster.AddListener(groundLayer.LayerIndex, Move);
        }
    }
}