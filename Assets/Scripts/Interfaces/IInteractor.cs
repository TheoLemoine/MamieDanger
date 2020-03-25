using UnityEngine;

namespace Interfaces
{
    public interface IInteractor
    {
        void RegisterToRange(IInteractable interactable);
        void DeregisterFromRange(IInteractable interactable);
        void RegisterInteracting(IInteractable interactable);
        void DeregisterInteracting(IInteractable interactable);
        GameObject GetGameObject();
    }
}