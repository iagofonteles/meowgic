using UnityEngine;
using UnityEngine.UI;

namespace Drafts.DataView
{
    public class CameraToRawImage : MonoBehaviour
    {
        public RenderTexture textureTemplate;
        public Camera camera;
        public RawImage image;

        public RenderTexture Texture { get; private set; }

        void Awake()
        {
            Texture = Instantiate(textureTemplate);
            camera.targetTexture = Texture;
            image.texture = Texture;
        }
    }
}