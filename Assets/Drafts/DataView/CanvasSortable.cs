using UnityEngine;

namespace Drafts.DataView {

	public interface ICanvasSortable {
		Transform transform { get; }
		int Size { get; }
	}

	public class CanvasSortable : MonoBehaviour, ICanvasSortable {
		[SerializeField] int size = 1;
		[SerializeField] bool deactivateOnAwake;
		[SerializeField] bool deactivateOnStart;
		public int Size => size;
		private void Awake() {
			var rect = GetComponent<RectTransform>();
			rect.anchoredPosition = Vector3.zero;
			if(deactivateOnAwake) gameObject.SetActive(false);
		}
		void Start() {
			if(deactivateOnStart) gameObject.SetActive(false);
		}
	}
}
