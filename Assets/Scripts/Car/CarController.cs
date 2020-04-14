using System;
using Interfaces;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField]
        private float motorTorque = 7f;

        [SerializeField] private WheelCollider wheelBackRight;
        [SerializeField] private WheelCollider wheelBackLeft;
        [SerializeField] private WheelCollider wheelFrontRight;
        [SerializeField] private WheelCollider wheelFrontLeft;

        private void FixedUpdate()
        {
            wheelBackLeft.motorTorque = motorTorque;
            wheelBackRight.motorTorque = motorTorque;
        }

        private void OnCollisionEnter(Collision other)
        {
            var killable = other.gameObject.GetComponent<IKillable>();
            killable?.Kill(gameObject);
        }
    }
}