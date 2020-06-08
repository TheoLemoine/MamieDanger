using Abstract;
using GameComponents.Player;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GameComponents.Box
{
    public class Grabbable : AInteracToggleBehaviour
    {
        
        [SerializeField] private float liftHeight = 0f;
        [SerializeField] private float distanceFromGrabber = 2f;
        [SerializeField] private float movementSmoothing = .15f;

        [SerializeField] private Transform handleRight;
        [SerializeField] private Transform handleLeft;
        
        private Rigidbody _rb;
        private Transform _transform;
        
        // components to desactivate
        private NavMeshObstacle _navObstacle;

        private Transform _grabber;
        private IInteractor _interactor;
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
                
                _rb.MovePosition(Vector3.Lerp(_transform.position, targetPos, movementSmoothing));
                _rb.MoveRotation(Quaternion.Lerp(_transform.rotation, _grabber.rotation, movementSmoothing));
            }
        }

        protected override void InteractStart(IInteractor interactor)
        {
            _interactor = interactor;
            _grabber = interactor.GetGameObject().GetComponent<Transform>();
            
            // stop
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            
            // stay in place
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rb.useGravity = false;

            // dont prevent player from walking
            _navObstacle.enabled = false;

            if (_interactor is PlayerInteractor playerInteractor)
            {
                var ik = playerInteractor.GetIkOverride();
                
                ik.SetGoal(AvatarIKGoal.RightHand, handleRight);
                ik.SetGoal(AvatarIKGoal.LeftHand, handleLeft);
            }
        }

        protected override void InteractStop()
        {
            _grabber = null;
            
            // move freely
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
            
            _navObstacle.enabled = true;
            
            if (_interactor is PlayerInteractor playerInteractor)
            {
                var ik = playerInteractor.GetIkOverride();
                
                ik.UnsetGoal(AvatarIKGoal.RightHand);
                ik.UnsetGoal(AvatarIKGoal.LeftHand);
            }
            
            _interactor = null;
        }
    }
}