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
                _isInteracting = true;
            }
            else
            {
                InteractStop(interactor);
                _isInteracting = false;
            }
        }

        protected bool IsInteracting()
        {
            return _isInteracting;
        }

        protected abstract void InteractStart(IInteractor interactor);

        protected abstract void InteractStop(IInteractor interactor);

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
