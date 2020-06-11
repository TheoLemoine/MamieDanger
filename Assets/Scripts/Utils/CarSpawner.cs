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
        [SerializeField] private float delay = 0f;
        [SerializeField] private float carSpeed = 2;
        [SerializeField] private float carMotorTorque = 1000;

        private Transform _transform;
        private void Start()
        {
            _transform = GetComponent<Transform>();
            
            StartCoroutine(InstantiatePrefabs());
        }

        private IEnumerator InstantiatePrefabs()
        {
            yield return new WaitForSeconds(delay);
            
            for (;;)
            {
                var prefab = prefabList[Random.Range(0, prefabList.Count)];
                
                var instantiated = Instantiate(prefab, _transform);
                var controller = instantiated.GetComponent<CarController>();
                
                controller.targetSpeed = carSpeed;
                controller.motorTorque = carMotorTorque;
                
                yield return new WaitForSeconds(period);
            }
        }
    }
}