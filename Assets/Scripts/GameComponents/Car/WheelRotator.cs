using System;
using UnityEngine;

namespace Car
{
    [RequireComponent(typeof(WheelCollider))]
    public class WheelRotator : MonoBehaviour
    {
        private MeshFilter _mesh;
        private WheelCollider _wheelCollider;
        private Transform _transform;

        private void Start()
        {
            _wheelCollider = GetComponent<WheelCollider>();
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            //_transform.Rotate(_wheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        }
    }
}