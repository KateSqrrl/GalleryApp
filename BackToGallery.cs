using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BackToGallery : MonoBehaviour
{
    public void OnBackButtonDown()
    {
        SceneManager.LoadScene("Gallery");
    }
}
