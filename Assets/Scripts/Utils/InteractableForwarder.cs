using System;
using UnityEngine;
using Interfaces;

namespace Utils
{
    public class InteractableForwarder : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject to;
        
        private IInteractable _next;

        private void Awake()
        {
            _next = to.GetComponent<IInteractable>();
        }

        public void InteractStart(GameObject interactor)
        {
            _next.InteractStart(interactor);
        }

        public void InteractStop(GameObject interactor)
        {
            _next.InteractStop(interactor);
        }
    }
}