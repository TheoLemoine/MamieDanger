using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils.Attributes;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

namespace Utils
{
    public class UpdateNavMeshOnCollide : MonoBehaviour
    {

        [SerializeField] private GameObject surfaceContainerObject;
        [SerializeField] private float angleLimit = 60f;

        private NavMeshModifier _navMeshModifier;
        private NavMeshSurface[] _navMeshSurfaces;
        
        private void Start()
        {
            _navMeshSurfaces = surfaceContainerObject.GetComponents<NavMeshSurface>();
            _navMeshModifier = GetComponent<NavMeshModifier>();
        }

        private void Update()
        {
            var angle = transform.rotation.eulerAngles.x;
            var angleIsPassed = angle > angleLimit || angle < -angleLimit;
            _navMeshModifier.ignoreFromBuild = !angleIsPassed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(_navMeshModifier.ignoreFromBuild)
                return;
            
            foreach (var navMeshSurface in _navMeshSurfaces)
            {
                navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            }
        }

    }
}