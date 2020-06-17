using System;
using UnityEngine;

namespace UI
{
    public class RendererGroup : MonoBehaviour
    {
        [SerializeField] private bool useParent = true;
        [SerializeField] private GameObject parentObject;
        [SerializeField] private Renderer[] rendererArray;
        private int[] _origLayers;
        private Material[] _origMaterial;

        private void Start()
        {
            if (useParent)
            {
                if (parentObject == null) throw new NullReferenceException("No parent set for this renderer group");
                rendererArray = parentObject.GetComponentsInChildren<Renderer>();
            }

            FillOrigArrays();
        }

        private void FillOrigArrays()
        {
            _origLayers = new int[rendererArray.Length];
            _origMaterial = new Material[rendererArray.Length];
            for (var i = 0; i < rendererArray.Length; i++)
            {
                _origLayers[i] = rendererArray[i].gameObject.layer;
                _origMaterial[i] = rendererArray[i].material;
            }
        }

        public void UseParent(GameObject parent)
        {
            parentObject = parent;
            useParent = true;
            rendererArray = parentObject.GetComponentsInChildren<Renderer>();
            FillOrigArrays();
        }
    
        public Renderer[] GetRenderers()
        {
            return rendererArray;
        }

        public void SetLayers(int layerInt)
        {
            foreach (var rendererComponent in rendererArray)
            {
                rendererComponent.gameObject.layer = layerInt;
            }
        }

        public void ResetLayers()
        {
            for (var i = 0; i < rendererArray.Length; i++)
            {
                rendererArray[i].gameObject.layer = _origLayers[i];
            }
        }

        public void ChangeMaterial(Material material)
        {
            for (var i = 0; i < rendererArray.Length; i++)
            {
                rendererArray[i].material = material;
            }
        }

        public void ResetMaterial()
        {
            for (var i = 0; i < rendererArray.Length; i++)
            {
                rendererArray[i].material = _origMaterial[i];
            }
        }
    }
}