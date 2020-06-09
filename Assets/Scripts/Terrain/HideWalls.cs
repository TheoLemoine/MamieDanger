 using UnityEngine;
 using System.Collections.Generic;
 using UI;
 using Utils;

 namespace Terrain 
 {
         
     public class HideWalls : MonoBehaviour
     {
         [SerializeField] private Transform playerTransform;
         [SerializeField] private Transform stencilSphereTransform;
         [SerializeField] private float sphereDistance = 40f;
         [SerializeField] private SingleUnityLayer occlusionLayer;
         [SerializeField] private SingleUnityLayer targetLayer;

         [SerializeField] private Material seeThroughMaterial;
         [SerializeField] private string colorProperty;
         
         [Range(0f, 1f)][SerializeField] private float minOpacity;
         [Range(0f, 1f)][SerializeField] private float maxOpacity;
         
         [Range(0f, 1f)][SerializeField] private float minFractionDistance;
         [Range(0f, 1f)][SerializeField] private float maxFractionDistance;

         
         private Transform _cameraTransform;
         private List<RendererGroup> _hiddenObjects;
         private float _rayDistance;

         private void Start()
         {
             _cameraTransform = transform;
             _hiddenObjects = new List<RendererGroup>();
         }
     
         private void Update()
         {
             MoveSphere();
             var hits = Raycast();
             HideHitObjects(hits);
             RevealPreviouslyHitObjects(hits);
         }
         

         private RaycastHit[] Raycast()
         {
             var playerPos = playerTransform.position;
             var direction = _cameraTransform.position -  playerPos;
             _rayDistance = direction.magnitude;

             var layerMask = 1 << occlusionLayer.LayerIndex;

             return Physics.RaycastAll(playerPos, direction, _rayDistance, layerMask);
         }

         private void HideHitObjects(RaycastHit[] hits)
         {
             var closestCollideDist = _rayDistance;
             foreach (var currentHit in hits)
             {
                 if (currentHit.distance < closestCollideDist) closestCollideDist = currentHit.distance;
                 if (currentHit.transform.TryGetComponent<RendererGroup>(out var currentGroup) && !_hiddenObjects.Contains(currentGroup))
                 {
                     _hiddenObjects.Add(currentGroup);
                     currentGroup.SetLayers(targetLayer.LayerIndex);
                 }
             }
             SetOpacity(closestCollideDist);
         }

         private void SetOpacity(float closestCollideDist)
         {
             var closestFrac = 1f - closestCollideDist / _rayDistance;
             var baseColor = seeThroughMaterial.GetColor(colorProperty);
             baseColor.a = closestFrac.Remap(minFractionDistance, maxFractionDistance, minOpacity, maxOpacity);
             seeThroughMaterial.SetColor(colorProperty, baseColor);
         }

         private void RevealPreviouslyHitObjects(RaycastHit[] hits)
         {
             // Store renderer group to reduce getComponent calls
             var hitGroups = new List<RendererGroup>();
             foreach (var hit in hits)
                 if (hit.transform.TryGetComponent<RendererGroup>(out var currentGroup))
                     hitGroups.Add(currentGroup);
             
             for (var i = 0; i < _hiddenObjects.Count; i++)
             {
                 var hiddenObject = _hiddenObjects[i];

                 var isHit = false;
                 foreach (var group in hitGroups)
                 {
                     if (group != hiddenObject) continue;
                     isHit = true;
                     break;
                 }
                 
                 if (isHit) continue;
                 hiddenObject.ResetLayers();
                 _hiddenObjects.RemoveAt(i);
                 i--;
             }
         }

         private void MoveSphere()
         {
             var camPos = _cameraTransform.position;
             var camToPlayer = playerTransform.position - camPos;
             var camToSphere = camToPlayer.normalized * sphereDistance;
             stencilSphereTransform.position = camPos + camToSphere;
         }
     }
 }