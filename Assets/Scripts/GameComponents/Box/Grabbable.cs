using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Abstract;
using UnityEngine.AI;

namespace Box
{
    public class Grabbable : AInteracToggleBehaviour
    {
        
        [SerializeField] private float liftHeight = 0f;
        [SerializeField] private float distanceFromGrabber = 2f;
        [SerializeField] private float movementSmoothing = .15f;
        
        private Rigidbody _rb;
        private Transform _transform;
        
        // components to desactivate
        private NavMeshObstacle _navObstacle;

        private Transform _grabber;
        private Vector3 _relativeTargetPos;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();

            _navObstacle = GetComponent<NavMeshObstacle>();

            _relativeTargetPos = new Vector3(0, liftHeight, distanceFromGrabber);
        }
        
        private void FixedUpdate()
        {
            if (IsInteracting())
            {
                var targetPos = _grabber.TransformPoint(_relativeTargetPos);

                _rb.MovePosition(targetPos);
                _rb.MoveRotation(Quaternion.Lerp(_transform.rotation, _grabber.rotation, 0.2f));
            }
        }

        protected override void InteractStart(IInteractor interactor)
        {
            _grabber = interactor.GetGameObject().GetComponent<Transform>();
            
            // stop
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            
            // stay in place
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rb.useGravity = false;

            // dont prevent player from walking
            _navObstacle.enabled = false;
        }

        protected override void InteractStop()
        {
            _grabber = null;
            
            // move freely
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
            
            _navObstacle.enabled = true;
        }
    }
}