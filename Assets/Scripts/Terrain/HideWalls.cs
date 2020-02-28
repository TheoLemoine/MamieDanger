 using UnityEngine;
 using System.Collections.Generic;
 using UnityEngine.Serialization;

 namespace Terrain
 {
     
 public class HideWalls : MonoBehaviour
 {
     //The player to shoot the ray at
     [SerializeField]
     public Transform playerTransform;
     //The camera to shoot the ray from
     [SerializeField]
     public Transform cameraTransform;
     //Layers to hide
     [SerializeField]
     public LayerMask layerMask;
     
     //List of all objects that we have hidden.
     private List<Transform> _hiddenObjects;

     private void Start()
     {
         //Initialize the list
         _hiddenObjects = new List<Transform>();
     }
 
     private void Update()
     {
         //Find the direction from the camera to the player
         var direction = playerTransform.position - cameraTransform.position;
 
         //The magnitude of the direction is the distance of the ray
         var distance = direction.magnitude;

         //Raycast and store all hit objects in an array. Also include the layermaks so we only hit the layers we have specified
         RaycastHit[] hits = Physics.RaycastAll(cameraTransform.position, direction, distance, layerMask);
 
         //Go through the objects
         for (int i = 0; i < hits.Length; i++)
         {
             var currentHit = hits[i].transform;
 
             //Only do something if the object is not already in the list
             if (!_hiddenObjects.Contains(currentHit))
             {
                 //Add to list and disable renderer
                 _hiddenObjects.Add(currentHit);
                 currentHit.GetComponent<Renderer>().enabled = false;
             }
         }
 
         //clean the list of objects that are in the list but not currently hit.
         for (int i = 0; i < _hiddenObjects.Count; i++)
         {
             var isHit = false;
             //Check every object in the list against every hit
             for (int j = 0; j < hits.Length; j++)
             {
                 if (hits[j].transform == _hiddenObjects[i])
                 {
                     isHit = true;
                     break;
                 }
             }
 
             //If it is not among the hits
             if (!isHit)
             {
                 //Enable renderer, remove from list, and decrement the counter because the list is one smaller now
                 Transform wasHidden = _hiddenObjects[i];
                 wasHidden.GetComponent<Renderer>().enabled = true;
                 _hiddenObjects.RemoveAt(i);
                 i--;
             }
         }
         
     }
 }
 }