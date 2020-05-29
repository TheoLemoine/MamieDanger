using System.Collections;
using System.Collections.Generic;
using Global.Input;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerPlayerRaycast : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        InputManager.PlayerRaycaster.Raycast();
    }
}
