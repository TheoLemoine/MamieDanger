using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utils.Attributes;

namespace GameComponents.PickUp
{
    public class PickUp : MonoBehaviour
    {
        [Serializable] public class PickUpEvent : UnityEvent<PickUp> { }
        [SerializeField] private PickUpEvent onPickedUp;

        [SerializeField] [TagSelector] private string playerTag;
        [SerializeField] public string pickUpId;
        public bool IsPickedUp { get; private set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if(IsPickedUp || !other.CompareTag(playerTag)) return;
            
            onPickedUp.Invoke(this);
            IsPickedUp = true;
        }
    }
}