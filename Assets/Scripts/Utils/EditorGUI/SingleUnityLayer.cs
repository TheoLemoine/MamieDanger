using UnityEditor;
using UnityEngine;

namespace Utils.EditorGUI
{
 
    [System.Serializable]
    public class SingleUnityLayer
    {
        [SerializeField]
        private int layerIndex = 0;
        public int LayerIndex => layerIndex;
        public int Mask => 1 << layerIndex;

        public void Set(int newLayerIndex)
        {
            if (newLayerIndex > 0 && newLayerIndex < 32)
            {
                layerIndex = newLayerIndex;
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(SingleUnityLayer))]
    public class SingleUnityLayerPropertyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(position, GUIContent.none, property);
            
            var layerIndex = property.FindPropertyRelative("layerIndex");
            position = UnityEditor.EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            if (layerIndex != null)
            {
                layerIndex.intValue = UnityEditor.EditorGUI.LayerField(position, layerIndex.intValue);
            }
            
            UnityEditor.EditorGUI.EndProperty();
        }
    }
}