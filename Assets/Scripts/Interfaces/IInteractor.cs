using UnityEngine;

namespace Interfaces
{
    public interface IInteractor
    {
        void AddInRange(int gameObjectId, IInteractable interactable);
        void RemoveFromRange(int gameObjectId);
        bool IsInteracting();
    }
}