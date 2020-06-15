using System.Linq;
using UnityEditor;

namespace Editor
{
    public class BakingScripts
    {

        [MenuItem("Scripts/BakeAllScenes")]
        public static void BakeAllScenes()
        {
            Lightmapping.BakeMultipleScenes(EditorBuildSettings.scenes.Select(scene => scene.path).ToArray());
        }
        
    }
}