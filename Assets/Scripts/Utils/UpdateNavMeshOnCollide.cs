using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
        
        private static bool _buildCoroutineWasLaunched;

        private void Awake()
        {
            _buildCoroutineWasLaunched = false;
        }

        private void Start()
        {
            _navMeshSurfaces = surfaceContainerObject.GetComponents<NavMeshSurface>();
            _navMeshModifier = GetComponent<NavMeshModifier>();
            
            if(_buildCoroutineWasLaunched) return;
            
            foreach (var navMeshSurface in _navMeshSurfaces)
            {
                navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            }
            
            _buildCoroutineWasLaunched = true;
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