using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageData
{
    public string base64Texture;
}

public static class ImageManager
{
    public static string ToJson(Texture2D texture)
    {
        byte[] textureBytes = texture.EncodeToPNG();
        ImageData imageData = new ImageData { base64Texture = System.Convert.ToBase64String(textureBytes) };
        return JsonUtility.ToJson(imageData);
    }

    public static Texture2D FromJson(string jsonString)
    {
        ImageData imageData = JsonUtility.FromJson<ImageData>(jsonString);
        byte[] textureBytes = System.Convert.FromBase64String(imageData.base64Texture);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(textureBytes);

        return texture;
    }


}
