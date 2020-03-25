using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Interfaces
{
    public interface IInteractable
    {
        void Interact(IInteractor interactor);
        void StopInteraction();
        GameObject GetGameObject();
    }

    public static class InteractableExtensions
    {
        public static int GetGameObjectId(this IInteractable interactable)
        {
            return interactable.GetGameObject().GetInstanceID();
        }
    }
}