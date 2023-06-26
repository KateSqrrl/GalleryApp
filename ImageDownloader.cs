using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageDownloader : MonoBehaviour, IPointerClickHandler
{
    public GameObject imagePrefab;
    public Transform contentTransform;

    public static string selectedImageJson;

    private void Start()
    {
        ImageLoader.instance.LoadImages(66, OnImageLoaded);
    }

    private void OnImageLoaded(Texture2D texture)
    {
        GameObject newImageObject = Instantiate(imagePrefab, contentTransform);
        RawImage rawImage = newImageObject.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.texture = texture;
        }

        EventTrigger eventTrigger = newImageObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnPointerClick((PointerEventData)eventData); });
        eventTrigger.triggers.Add(entry);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RawImage clickedImage = eventData.pointerPress.GetComponent<RawImage>();
        if (clickedImage != null)
        {
            LoadImageScene(clickedImage);
        }
    }

    private void LoadImageScene(RawImage clickedImage)
    {
        selectedImageJson = ImageManager.ToJson((Texture2D)clickedImage.texture);
        SceneManager.LoadScene("View");
    }

}