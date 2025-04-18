using UnityEditor;
using UnityEngine;
using Drafts;
using Drafts.Editor;

namespace Meowgic.Match.Editor
{
    [CustomPropertyDrawer(typeof(Catalyst))]
    public class CatalystDrawer : PropertyDrawer
    {
        private const float EffectSize = 24f;
        private static readonly AssetSearchSettings<Effect> SearchSettings = new("Assets/Meowgic/Database");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EffectSize;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var catalystBase = property.FindPropertyRelative("catalystBase");
            var effects = property.FindPropertyRelative("effects").FindPropertyRelative("array");

            const int slots = 2;
            position.width -= EffectSize * (slots+1);
            EditorGUI.PropertyField(position, catalystBase, label);

            position.x += position.width;
            position.width = EffectSize;

            if (catalystBase.objectReferenceValue is CatalystBase @base)
            {
                var tex = @base.Icon.texture;
                if (tex) GUI.DrawTexture(position, tex, ScaleMode.ScaleToFit);
            }
            position.x += position.width;
            
            if (effects.arraySize != slots)
            {
                effects.arraySize = slots;
                property.serializedObject.ApplyModifiedProperties();
                return;
            }

            for (var i = 0; i < slots; i++)
            {
                var eff = effects.GetArrayElementAtIndex(i);

                var i1 = i;
                var click = Clicked(position);
                if (click == 0) SearchSettings.Search(null, e => SetEffect(i1, e));
                if (click == 1) eff.objectReferenceValue = null;

                var effect = eff.objectReferenceValue as Effect;
                var tex = effect?.Icon.texture;
                if (tex) GUI.DrawTexture(position, tex, ScaleMode.ScaleToFit);
                else GUI.Button(position, "+");

                position.x += position.width;
            }

            void SetEffect(int index, Effect e)
            {
                var eff = effects.GetArrayElementAtIndex(index);
                eff.objectReferenceValue = e;
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        int Clicked(Rect rect)
        {
            var e = Event.current;
            if (!rect.Contains(e.mousePosition)) return -1;
            if (e.type != EventType.MouseDown) return -1;
            return e.button;
        }
    }
}