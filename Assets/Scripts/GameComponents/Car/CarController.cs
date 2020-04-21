using System;
using Interfaces;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField]
        private float targetSpeed = 5f;
        
        [SerializeField]
        private float motorTorque = 1000f;

        [SerializeField] private WheelCollider wheelBackRight;
        [SerializeField] private WheelCollider wheelBackLeft;
        [SerializeField] private WheelCollider wheelFrontRight;
        [SerializeField] private WheelCollider wheelFrontLeft;

        private Rigidbody _rb;
        private Transform _transform;
        
        private void Start()
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
            killable?.Kill(gameObject);
        }
    }
}