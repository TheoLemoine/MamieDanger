using Global.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Animations
{
    public class TriggerPlayerRaycast : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            InputManager.PlayerRaycaster.Raycast();
        }
    }
}
