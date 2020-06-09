using System.Collections.Generic;

namespace Global.Save
{
    public class SaveData
    {
        public SaveData()
        {
            Levels = new Dictionary<string, LevelResults>();
        }

        public struct LevelResults
        {
            public bool Finished;
            public List<string> CoinsPicked; 
        }

        public Dictionary<string, LevelResults> Levels;
        
    }
}