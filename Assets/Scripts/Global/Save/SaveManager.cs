using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Global.Save
{
    public class SaveManager : MonoBehaviour
    {

        [SerializeField]
        private string saveFileName = "player_save.bin";
        
        public static SaveManager Instance { get; private set; }
        public SaveData Data { get; private set; }

        private string SaveFilePath => $"{Application.persistentDataPath}/{saveFileName}";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            if (Instance != null)
            {
                Debug.LogError("a SaveManager already exists ! Destroying current...");
                Destroy(gameObject);
            }
            
            Instance = this;
            Data = Retrieve();
        }

        private SaveData Retrieve()
        {
            if (!File.Exists(SaveFilePath))
            {
                return new SaveData();
            }
            
            var formatter = new BinaryFormatter();
            
            using (var stream = new FileStream(SaveFilePath, FileMode.Open))
            {
                return formatter.Deserialize(stream) as SaveData;
            }
        }

        public void Persist()
        {
            var formatter = new BinaryFormatter();

            using (var stream = new FileStream(SaveFilePath, FileMode.Create))
            {
                formatter.Serialize(stream, Data);
            }
        }
    }
}