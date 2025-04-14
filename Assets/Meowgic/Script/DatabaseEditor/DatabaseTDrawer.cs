#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Meowgic
{
    [CustomPropertyDrawer(typeof(Database<>), true)]
    public class DatabaseTDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var items = property.FindPropertyRelative("items");
            return EditorGUI.GetPropertyHeight(items, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var items = property.FindPropertyRelative("items");
            EditorGUI.PropertyField(position, items, label, true);
            EditorGUI.EndProperty();
        }
    }
}
#endif