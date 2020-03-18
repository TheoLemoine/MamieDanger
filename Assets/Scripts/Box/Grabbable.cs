using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Box
{
    public class Grabbable : MonoBehaviour, IInteractable
    {
        
        [SerializeField] private float liftHeight = 0f;
        [SerializeField] private float distanceFromGrabber = 2f;
        [SerializeField] private float movementSmoothing = .15f;
        
        private Rigidbody _rb;
        private Transform _transform;

        private Transform _grabber;
        private bool _isGrabbing;
        private Vector3 _relativeTargetPos;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();

            _relativeTargetPos = new Vector3(0, liftHeight, distanceFromGrabber);
        }
        
        private void FixedUpdate()
        {
            if (_isGrabbing)
            {
                var targetPos = _grabber.TransformPoint(_relativeTargetPos);

                _rb.MovePosition(Vector3.Lerp(_transform.position, targetPos, movementSmoothing));
                _rb.MoveRotation(Quaternion.Lerp(_transform.rotation, _grabber.rotation, movementSmoothing));
            }
        }

        public void InteractStart(GameObject interactor)
        {
            if(_isGrabbing) return;
            
            _grabber = interactor.GetComponent<Transform>();
            _isGrabbing = true;

            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rb.useGravity = false;
        }

        public void InteractStop()
        {
            if(!_isGrabbing) return;
            
            _grabber = null;
            _isGrabbing = false;
            
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }

        public bool IsInteracting()
        {
            return _isGrabbing && _grabber != null;
        }

        public GameObject GetInteractor()
        {
            return _grabber.gameObject;
        }
    }
}