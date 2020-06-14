using Global.Sound;
using Interfaces;
using UnityEngine;

namespace GameComponents.Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField]
        public float targetSpeed = 5f;

        [SerializeField]
        public float motorTorque = 1000f;

        [SerializeField] private WheelCollider wheelBackRight;
        [SerializeField] private WheelCollider wheelBackLeft;
        [SerializeField] private WheelCollider wheelFrontRight;
        [SerializeField] private WheelCollider wheelFrontLeft;

        private readonly string[] _bonks = { "Death Bonk 1", "Death Bonk 2" };
        private Rigidbody _rb;
        private Transform _transform;
        
        protected virtual void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            var relVel = _transform.InverseTransformDirection(_rb.velocity);
            
            if (relVel.z < targetSpeed)
            {
                wheelBackLeft.motorTorque = motorTorque;
                wheelBackRight.motorTorque = motorTorque;
            }
            else
            {
                wheelBackLeft.motorTorque = 0;
                wheelBackRight.motorTorque = 0;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            var killable = other.gameObject.GetComponent<IKillable>();
            if (killable != null)
            {
                SoundManager.PlayStatic(_bonks[Random.Range(0, _bonks.Length)]);
                killable.Kill(gameObject);
            }
        }
    }
}