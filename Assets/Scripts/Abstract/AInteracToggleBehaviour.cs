using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Abstract
{
    public abstract class AInteracToggleBehaviour : MonoBehaviour, IInteractable
    {
        private bool _isInteracting = false;

        public void Interact(IInteractor interactor)
        {
            if (!_isInteracting)
            {
                InteractStart(interactor);
                interactor.RegisterInteracting(this);
                _isInteracting = true;
            }
            else
            {
                InteractStop();
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
