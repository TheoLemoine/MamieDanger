using System;
using System.Collections;
using System.Collections.Generic;
using GameComponents.Car;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    
    
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabList;
        [SerializeField] private float period = 1f;
        [SerializeField] private bool overrideCarSpeed;
        [SerializeField] private float carSpeed;

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
                var prefab = prefabList[Random.Range(0, prefabList.Count)];
                
                var instantiated = Instantiate(prefab, _transform);
                
                if (overrideCarSpeed)
                    instantiated.GetComponent<CarController>().TargetSpeed = carSpeed;
                
                yield return new WaitForSeconds(period);
            }
        }
    }
}