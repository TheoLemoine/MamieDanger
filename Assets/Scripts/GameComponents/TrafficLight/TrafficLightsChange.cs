using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameComponents.TrafficLight
{
    public class TrafficLightsChange : MonoBehaviour
    {

        [SerializeField] private List<MeshRenderer> renderers;
        [SerializeField] public float secondsBetweenChange = 1f;

        private const string EmissionKeyword = "_EMISSION";

        private void Awake()
        {
            foreach (var meshRenderer in renderers)
            {
                var material = meshRenderer.material;
                material.DisableKeyword(EmissionKeyword);
            }

            StartCoroutine(ChangeLights());
        }

        private IEnumerator ChangeLights()
        {
            Material material = null;
            
            while (true) foreach (var meshRenderer in renderers)
            {
                // set off old material
                if(material != null) material.DisableKeyword(EmissionKeyword);
            
                // set on new material
                // and keep it for next loop
                material = meshRenderer.material;
                material.EnableKeyword(EmissionKeyword);
            
                yield return new WaitForSeconds(secondsBetweenChange);
            }
        }

    }
}