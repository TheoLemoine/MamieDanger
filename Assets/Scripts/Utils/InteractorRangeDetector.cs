using System;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utils
{
    public class InteractorRangeDetector : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject notify;
        
        [SerializeField] private SingleUnityLayer clickableLayer;
        [SerializeField] private SingleUnityLayer notClickableLayer;
        
        private IInteractable _targetInteractable;
        private IInteractor _notifyInteractor;

        private int _targetId;
        private int _notifyId;

        
        private void Start()
        {
            _targetInteractable = target.GetComponent<IInteractable>();
            _notifyInteractor = notify.GetComponent<IInteractor>();
            
            _targetId = target.GetInstanceID();
            _notifyId = notify.GetInstanceID();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetInstanceID() == _notifyId)
            {
                target.layer = clickableLayer.LayerIndex;
                _notifyInteractor.RegisterToRange(_targetInteractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetInstanceID() == _notifyId)
            {
                target.layer = notClickableLayer.LayerIndex;
                _notifyInteractor.DeregisterFromRange(_targetInteractable);
            }
        }
    }
}