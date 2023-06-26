using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadProgress;
    public Slider slider;
    public TextMeshProUGUI progressText;
    private float delayFactor = 0.7f;

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        loadProgress.SetActive(true);

        while (slider.value < 1)
        {
            float loadProgress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = Mathf.MoveTowards(slider.value, loadProgress, delayFactor * Time.deltaTime);
            progressText.text = Mathf.RoundToInt(slider.value * 100f) + "%";

            if (Mathf.Approximately(slider.value, 1))
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

