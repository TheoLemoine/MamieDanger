using System;
using UnityEngine;

namespace GameComponents.PickUp
{
    public class PickUpAnimate : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed;
        [SerializeField] private Vector3 oscillationSpeed;
        [SerializeField] private Vector3 oscillationAmplitude;

        [SerializeField] private Rigidbody targetRigidbody;
        private Transform _transform;
        
        public bool paused = false;

        private void Start()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            if (paused) return;
            
            var deltaRotation = Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime);
            targetRigidbody.MoveRotation(targetRigidbody.rotation * deltaRotation);

            var osciliationOffset = new Vector3(
                Mathf.Sin(oscillationSpeed.x * Time.fixedTime),
                Mathf.Sin(oscillationSpeed.y * Time.fixedTime),
                Mathf.Sin(oscillationSpeed.z * Time.fixedTime)
            );
            // vector multiplication in unity
            osciliationOffset.Scale(oscillationAmplitude);

            var worldOsciliationOffset = _transform.TransformPoint(osciliationOffset);
            targetRigidbody.MovePosition(worldOsciliationOffset);
        }

        public void Pause()
        {
            paused = true;
        }

        public void Play()
        {
            paused = false;
        }
    }
}