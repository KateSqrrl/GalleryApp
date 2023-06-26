using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ImageLoader : MonoBehaviour
{
    public static ImageLoader instance;

    private const string BaseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private Dictionary<string, Texture2D> imageCache;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        imageCache = new Dictionary<string, Texture2D>();
    }

    public void LoadImages(int totalImages, System.Action<Texture2D> callback)
    {
        int imageNumber = 1;
        LoadImageRecursive(imageNumber, totalImages, callback);
    }

    private void LoadImageRecursive(int imageNumber, int totalImages, System.Action<Texture2D> callback)
    {
        string imageUrl = GetImageUrl(imageNumber);
        if (imageCache.ContainsKey(imageUrl))
        {
            callback.Invoke(imageCache[imageUrl]);
            imageNumber++;
            if (imageNumber <= totalImages)
            {
                LoadImageRecursive(imageNumber, totalImages, callback);
            }
        }
        else
        {
            StartCoroutine(DownloadImage(imageUrl, (texture) =>
            {
                callback.Invoke(texture);
                imageNumber++;
                if (imageNumber <= totalImages)
                {
                    LoadImageRecursive(imageNumber, totalImages, callback);
                }
            }));
        }
    }

    private string GetImageUrl(int imageNumber)
    {
        return $"{BaseUrl}{imageNumber}.jpg";
    }

    private IEnumerator DownloadImage(string imageUrl, System.Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            imageCache[imageUrl] = texture;
            callback.Invoke(texture);
        }
        else
        {
            Debug.LogError($"Error downloading image: {request.error}");
        }
    }
}



