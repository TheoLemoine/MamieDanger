﻿ using System;
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

         [SerializeField] private Material seeThroughMaterial;
         [SerializeField] private string colorProperty;
         
         [Range(0f, 1f)][SerializeField] private float minOpacity;
         [Range(0f, 1f)][SerializeField] private float maxOpacity;
         
         [Range(0f, 1f)][SerializeField] private float minFractionDistance;
         [Range(0f, 1f)][SerializeField] private float maxFractionDistance;
         
         private List<Transform> _hiddenObjects;
         private float rayDistance;

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
             var playerPos = _playerTransform.position;
             var direction = cameraTransform.position -  playerPos;
             rayDistance = direction.magnitude;

             string[] layersToCheck = {wallLayerName, targetLayerName};

             return Physics.RaycastAll( playerPos, direction, rayDistance, LayerMask.GetMask(layersToCheck));
         }
         

         private void HideHitObjects(RaycastHit[] hits)
         {
             var closestCollideDist = rayDistance;
             foreach (var currentHit in hits)
             {
                 if (currentHit.distance < closestCollideDist) closestCollideDist = currentHit.distance;
                 var currentTransform = currentHit.transform;
                 if (!_hiddenObjects.Contains(currentTransform))
                 {
                     _hiddenObjects.Add(currentTransform);
                    currentTransform.gameObject.layer = LayerMask.NameToLayer(targetLayerName);
                 }
             }

             var closestFrac = 1f - closestCollideDist / rayDistance;
             var baseColor = seeThroughMaterial.GetColor(colorProperty);
             baseColor.a = closestFrac.Remap(minFractionDistance, maxFractionDistance, minOpacity, maxOpacity);
             seeThroughMaterial.SetColor(colorProperty, baseColor); 
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