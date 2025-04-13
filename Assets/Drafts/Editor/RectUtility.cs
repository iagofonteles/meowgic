using UnityEditor;
using UnityEngine;

namespace Drafts.Editor {
	public static class RectUtility {

		public static Rect NextY(this ref Rect rect, float height) {
			var r = rect;
			r.height = height;
			rect.height -= height;
			rect.y += height;
			return r;
		}

		public static Rect NextX(this ref Rect rect, float width) {
			var r = rect;
			r.width = width;
			rect.width -= width;
			rect.x += width;
			return r;
		}
		
		public static Rect NextY(this ref Rect rect, SerializedProperty p, bool includeChildren)
			=> rect.NextY(EditorGUI.GetPropertyHeight(p, includeChildren));

		public static Rect NextProperty(this ref Rect rect, SerializedProperty p, bool includeChildren) {
			var r = rect.NextY(p, includeChildren);
			EditorGUI.PropertyField(r, p);
			return r;
		}

		public static Rect NextLine(this ref Rect rect)
			=> rect.NextY(EditorGUIUtility.singleLineHeight);

		public static Rect GetLine(this ref Rect rect) {
			var r = rect;
			r.height = EditorGUIUtility.singleLineHeight;
			return r;
		}
	}
}