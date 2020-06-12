using System;
using System.Collections.Generic;
using Global.Sound;

namespace Global.Save
{
    [Serializable]
    public class SaveData
    {
        public SaveData()
        {
            levels = new Dictionary<string, LevelResults>();
        }

        [Serializable]
        public struct LevelResults
        {
            public bool finished;
            public List<string> coinsPicked; 
        }
        
        [Serializable]
        public struct Settings
        {
            public VolumeLevels volume; 
        }

        public Dictionary<string, LevelResults> levels;
        public Settings settings;

    }
}