using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        void InteractStart(GameObject interactor);
        void InteractStop(GameObject interactor);
    }
}