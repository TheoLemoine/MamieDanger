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
                plateRigidbody.AddForce(pushForce, ForceMode.VelocityChange);
            }
        }
    }
}