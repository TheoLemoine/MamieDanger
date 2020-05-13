using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

namespace Utils
{
    public class UpdateNavMeshOnCollide : MonoBehaviour
    {

        [SerializeField] private Bounds bounds;
        
        private Transform _transform;

        private List<NavMeshBuildSource> _navMeshSourceList = new List<NavMeshBuildSource>();
        private NavMeshData _navMeshData;
        private NavMeshDataInstance _navMeshDataInstance;
        
        private void Start()
        {
            _transform = GetComponent<Transform>();
            
            var source = new NavMeshBuildSource
            {
                shape = NavMeshBuildSourceShape.Mesh,
                sourceObject = GetComponent<MeshFilter>().sharedMesh,
                transform = _transform.localToWorldMatrix,
                area = 0,
            };
            
            _navMeshSourceList.Add(source);
        }

        private void OnEnable()
        {
            _navMeshData = new NavMeshData();
            _navMeshDataInstance = NavMesh.AddNavMeshData(_navMeshData);
        }

        private void OnDisable()
        {
            _navMeshDataInstance.Remove();
        }

        private void OnCollisionEnter(Collision other)
        {
            StartCoroutine(UpdateNavMeshes());
        }

        private IEnumerator UpdateNavMeshes()
        {
            var centeredBounds = new Bounds(_transform.TransformPoint(bounds.center), bounds.size);
            
            for (int i = 0; i < NavMesh.GetSettingsCount(); i++)
            {
                var settings = NavMesh.GetSettingsByIndex(i);
                yield return NavMeshBuilder.UpdateNavMeshDataAsync(_navMeshData, settings, _navMeshSourceList, centeredBounds);
            }
        } 

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.TransformPoint(bounds.center), bounds.size);
        }
    }
}