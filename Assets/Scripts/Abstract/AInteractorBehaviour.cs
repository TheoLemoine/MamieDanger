using System.Collections;
using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Abstract
{
    public abstract class AInteractorBehaviour : MonoBehaviour, IInteractor
    {
        protected Dictionary<int, IInteractable> _interactablesInRange = new Dictionary<int, IInteractable>();
        public IInteractable CurrentInteracting { get; protected set; }

        public void RegisterToRange(IInteractable interactable)
        {
            _interactablesInRange.Add(interactable.GetGameObjectId(), interactable);
        }

        public void DeregisterFromRange(IInteractable interactable)
        {
            _interactablesInRange.Remove(interactable.GetGameObjectId());
        }

        public void RegisterInteracting(IInteractable interactable)
        {
            CurrentInteracting?.StopInteraction();
            CurrentInteracting = interactable;
        }

        public void DeregisterInteracting(IInteractable interactable)
        {
            CurrentInteracting = null;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}

