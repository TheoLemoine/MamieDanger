using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Abstract
{
    
    public abstract class AInteracToggleBehaviour : MonoBehaviour, IInteractable
    {
        private bool _isInteracting = false;
        [SerializeField] private UnityEvent interactStartEvent; 
        [SerializeField] private UnityEvent interactStopEvent; 

        public void Interact(IInteractor interactor)
        {
            if (!_isInteracting)
            {
                InteractStart(interactor);
                interactStartEvent.Invoke();
                interactor.RegisterInteracting(this);
                _isInteracting = true;
            }
            else
            {
                InteractStop();
                interactStopEvent.Invoke();
                interactor.DeregisterInteracting(this);
                _isInteracting = false;
            }
        }

        public void StopInteraction()
        {
            if (IsInteracting())
            {
                _isInteracting = false;
                InteractStop();
            }
        }

        protected bool IsInteracting()
        {
            return _isInteracting;
        }

        protected abstract void InteractStart(IInteractor interactor);

        protected abstract void InteractStop();

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
