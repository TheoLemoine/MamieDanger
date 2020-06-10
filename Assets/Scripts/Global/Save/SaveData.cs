using System;
using System.Collections.Generic;

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

        public Dictionary<string, LevelResults> levels;
        
    }
}