using System;
using Controls;
using UnityEngine;

namespace Global.Input
{
    public class InputManager : MonoBehaviour
    {
        public static PlayerControls ActionMaps { get; private set; }
        public static bool IsReady { get; private set; }
        public static Raycaster PlayerRaycaster { get; private set; }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            ActionMaps = new PlayerControls();
            ActionMaps.Enable();
            
            IsReady = true;
        }

        private void Start()
        {
            PlayerRaycaster = new Raycaster();
        }

        private void OnDestroy()
        {
            ActionMaps.Disable();
            
            IsReady = false;
        }
    }
}