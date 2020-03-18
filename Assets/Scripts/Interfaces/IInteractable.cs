using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Interfaces
{
    public interface IInteractable
    {
        void InteractStart(GameObject interactor);
        void InteractStop();
        bool IsInteracting();
    }

    public static class InteractableExtentions
    {
        public static void ToggleInteract(this IInteractable interactable, GameObject interactor)
        {
            if (interactable.IsInteracting())
            {
                interactable.InteractStop();
            }
            else
            {
                interactable.InteractStart(interactor);
            }
        }
    }
}