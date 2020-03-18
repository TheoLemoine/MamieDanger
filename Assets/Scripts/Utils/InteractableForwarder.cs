using System;
using UnityEngine;
using Interfaces;

namespace Utils
{
    public class InteractableForwarder : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject forwardTo;
        
        private IInteractable _next;

        private void Start()
        {
            _next = forwardTo.GetComponent<IInteractable>();
        }

        public void InteractStart(GameObject interactor)
        {
            _next.InteractStart(interactor);
        }

        public void InteractStop()
        {
            _next.InteractStop();
        }

        public bool IsInteracting()
        {
            return _next.IsInteracting();
        }
    }
}