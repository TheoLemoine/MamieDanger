using Interfaces;
using UnityEngine;
using Abstract;

namespace Box
{
    public class Grabbable : AInteracToggleBehaviour
    {
        
        [SerializeField] private float liftHeight = 0f;
        [SerializeField] private float distanceFromGrabber = 2f;
        [SerializeField] private float movementSmoothing = .15f;
        
        private Rigidbody _rb;
        private Transform _transform;

        private Transform _grabber;
        private Vector3 _relativeTargetPos;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();

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
            _grabber = interactor.GetGameObject().GetComponent<Transform>();

            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rb.useGravity = false;
        }

        protected override void InteractStop(IInteractor interactor)
        {
            _grabber = null;
            
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }
    }
}