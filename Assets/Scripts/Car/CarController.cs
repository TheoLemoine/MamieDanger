using System;
using Interfaces;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 7f;

        private Transform _transform;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        
        private void FixedUpdate()
        {
            var movement = Vector3.forward * (speed * Time.fixedDeltaTime);
            _rb.MovePosition(_transform.TransformPoint(movement));
        }

        private void OnCollisionEnter(Collision other)
        {
            var killable = other.gameObject.GetComponent<IKillable>();
            killable?.Kill(gameObject);
        }
    }
}