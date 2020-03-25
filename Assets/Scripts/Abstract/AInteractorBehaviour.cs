using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Abstract
{
    public abstract class AInteractorBehaviour : MonoBehaviour, IInteractor
    {
        protected Dictionary<int, IInteractable> _interactablesInRange = new Dictionary<int, IInteractable>();
        protected Dictionary<int, IInteractable> _interactingInteractables = new Dictionary<int, IInteractable>();
            
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
            _interactingInteractables.Add(interactable.GetGameObjectId(), interactable);
        }

        public void DeregisterInteracting(IInteractable interactable)
        {
            _interactingInteractables.Remove(interactable.GetGameObjectId());
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}

