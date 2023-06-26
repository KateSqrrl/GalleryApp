using UnityEngine;
using UnityEngine.UI;


public class DisplayImage : MonoBehaviour
{
    void Start()
    {
        RawImage rawImage = GetComponent<RawImage>();

        string textureJson = ImageDownloader.selectedImageJson;

        Texture2D texture = ImageManager.FromJson(textureJson);

        rawImage.texture = texture;
    }
}

