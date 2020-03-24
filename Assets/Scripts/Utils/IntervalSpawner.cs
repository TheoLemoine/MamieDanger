using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class IntervalSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float period = 1f;

        private Transform _transform;
        private void Start()
        {
            _transform = GetComponent<Transform>();
            
            StartCoroutine(InstantiatePrefabs());
        }

        private IEnumerator InstantiatePrefabs()
        {
            for (;;)
            {
                Instantiate(prefab, _transform);
                yield return new WaitForSeconds(period);
            }
        }
    }
}