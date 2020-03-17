using Interfaces;
using UnityEngine;

namespace Box
{
    public class Grabbable : MonoBehaviour, IInteractable
    {
        
        [SerializeField] private float liftHeight = 0f;
        [SerializeField] private float distanceFromGrabber = 2f;
        
        private Rigidbody _rb;

        private Transform _grabber;
        private bool _isGrabbing;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            if (_isGrabbing)
            {
                var relativeTargetPos = new Vector3(0, liftHeight, distanceFromGrabber);
                var targetPos = _grabber.TransformPoint(relativeTargetPos);
                
                _rb.MovePosition(targetPos);
                _rb.MoveRotation(_grabber.rotation);
            }
        }

        public void InteractStart(GameObject interactor)
        {
            _grabber = interactor.GetComponent<Transform>();
            _isGrabbing = true;

            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rb.useGravity = false;
        }

        public void InteractStop(GameObject interactor)
        {
            _grabber = null;
            _isGrabbing = false;
            
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }

    }
}