 using System;
 using UnityEngine;
 using System.Collections.Generic;
 using Utils;

 namespace Terrain
 {
         
     public class HideWalls : MonoBehaviour
     {
         private Transform _playerTransform;
         [SerializeField] private Transform cameraTransform;
         [SerializeField] private string wallLayerName;
         [SerializeField] private string targetLayerName;
         
         private List<Transform> _hiddenObjects;

         private void Start()
         {
             _playerTransform = transform;
             _hiddenObjects = new List<Transform>();
         }
     
         private void Update()
         {
             var hits = Raycast();
             HideHitObjects(hits);
             RevealPreviouslyHitObjects(hits);
         }
         

         private RaycastHit[] Raycast()
         {
             var cameraPos = cameraTransform.position;
             var direction = _playerTransform.position - cameraPos;
             var distance = direction.magnitude;

             string[] layersToCheck = {wallLayerName, targetLayerName};

             return Physics.RaycastAll(cameraPos, direction, distance, LayerMask.GetMask(layersToCheck));
         }
         

         private void HideHitObjects(RaycastHit[] hits)
         {
             foreach (var currentHit in hits)
             {
                 var currentTransform = currentHit.transform;
                 if (!_hiddenObjects.Contains(currentTransform))
                 {
                     _hiddenObjects.Add(currentTransform);
                    currentTransform.gameObject.layer = LayerMask.NameToLayer(targetLayerName);
                 }
             }
         }
         

         private void RevealPreviouslyHitObjects(RaycastHit[] hits)
         {
             for (var i = 0; i < _hiddenObjects.Count; i++)
             {
                 var hiddenObject = _hiddenObjects[i];

                 var isHit = false;
                 foreach (var hit in hits)
                 {
                     if (hit.transform != hiddenObject) continue;
                     isHit = true;
                     break;
                 }
                 
                 if (isHit) continue;
                 hiddenObject.gameObject.layer = LayerMask.NameToLayer(wallLayerName);
                 _hiddenObjects.RemoveAt(i);
                 i--;
             }
         }
     }
 }