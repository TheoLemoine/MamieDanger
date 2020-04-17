using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Global.Input
{
    public delegate void RaycastCallback(RaycastHit hit);

    public class Raycaster
    {
        private Dictionary<int, List<RaycastCallback>> _callbacksDict = new Dictionary<int, List<RaycastCallback>>();
        private Vector2 _pointer;
        private Camera _cam;
        private float _maxRaycastDistance = 100f;

        public Raycaster()
        {
            if (InputManager.IsReady)
            {
                InputManager.ActionMaps.Player.UpdatePointer.performed += UpdatePointer;
                InputManager.ActionMaps.Player.Move.started += Move;
            }
        }
            
        public void AddListener(int layer, RaycastCallback callback)
        {
            if (!_callbacksDict.ContainsKey(layer)) _callbacksDict[layer] = new List<RaycastCallback>();
            if (_callbacksDict[layer].Contains(callback))
                Debug.LogWarningFormat("{0} is already registered at index {1}", callback, layer);
            else
                _callbacksDict[layer].Add(callback);
        }
        
        public void RemoveListener(int layer, RaycastCallback callback)
        {
            if (!_callbacksDict.ContainsKey(layer)) return;
            var cbs = _callbacksDict[layer];
            if (!cbs.Contains(callback)) return;
            cbs.Remove(callback);
            if (cbs.Count == 0) _callbacksDict.Remove(layer);
        }

        private void UpdatePointer(InputAction.CallbackContext context)
        {
            _pointer = context.ReadValue<Vector2>();
        }

        private void Move(InputAction.CallbackContext context)
        {
            var ray = GetCamera().ScreenPointToRay(_pointer);
            var layersMask = 0;
            var layers = _callbacksDict.Keys;
            
            foreach (var layer in layers)
                layersMask |= 1 << layer;

            if (Physics.Raycast(ray, out var hit, _maxRaycastDistance, layersMask))
            {
                var hitLayer = hit.collider.gameObject.layer;
                if (_callbacksDict.TryGetValue(hitLayer, out var callbacks))
                {
                    foreach (var raycastCallback in callbacks)
                        raycastCallback(hit);
                }
            }
        }

        private Camera GetCamera()
        {
            if(_cam == null) _cam = Camera.main;
            return _cam;
        }
    }
}
