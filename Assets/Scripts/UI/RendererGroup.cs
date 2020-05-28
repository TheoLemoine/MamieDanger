using System;
using UnityEngine;

public class RendererGroup : MonoBehaviour
{
    [SerializeField] private bool useParent = true;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Renderer[] rendererArray;
    private int[] _origLayers;

    private void Start()
    {
        if (useParent)
        {
            if (parentObject == null) throw new NullReferenceException("No parent set for this renderer group");
            rendererArray = parentObject.GetComponentsInChildren<Renderer>();
        }

        _origLayers = new int[rendererArray.Length];
        for (var i = 0; i < rendererArray.Length; i++)
        {
            _origLayers[i] = rendererArray[i].gameObject.layer;
        }
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
}