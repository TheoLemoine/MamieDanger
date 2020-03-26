using System;
using UnityEngine;

namespace Utils
{
    
    [Serializable]
    public class SingleUnityLayer
    {
        [SerializeField]
        private int layerIndex = 0;
        public int LayerIndex => layerIndex;
        public int Mask => 1 << layerIndex;

        public void Set(int newLayerIndex)
        {
            if (newLayerIndex > 0 && newLayerIndex < 32)
            {
                layerIndex = newLayerIndex;
            }
        }
    }
}