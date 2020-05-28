using System;
using UnityEngine;
using Utils.Attributes;

namespace GameComponents.PlateAndPole
{
    public class PushPlate : MonoBehaviour
    {
        
        [SerializeField] private Rigidbody plateRigidbody;
        [SerializeField] private Vector3 pushForce;
        [SerializeField] [TagSelector] private string playerTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                var correctedForce = plateRigidbody.gameObject.transform.TransformDirection(pushForce);
                plateRigidbody.AddForce(correctedForce, ForceMode.VelocityChange);
            }
        }
    }
}