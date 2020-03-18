using System;
using Interfaces;
using Player;
using UnityEngine;

namespace Utils
{
    public class InteractorRangeDetector : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject notify;

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
                _notifyInteractor.AddInRange(_targetId, _targetInteractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetInstanceID() == _notifyId)
            {
                _notifyInteractor.RemoveFromRange(_targetId);
            }
        }
    }
}