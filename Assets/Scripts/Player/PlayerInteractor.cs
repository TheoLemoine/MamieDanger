using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.gameObject.GetComponent<IInteractable>();
            interactable.InteractStart(this.gameObject);
        }
    }
}
