using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestureHandler : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool swipeDetected;
    private float swipeThreshold = 100f;

    private ScrollRect scrollRect;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeDetected = false;
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && !swipeDetected)
            {
                currentTouchPosition = touch.position;
                float deltaX = currentTouchPosition.x - startTouchPosition.x;
                float deltaY = currentTouchPosition.y - startTouchPosition.y;

                if (Mathf.Abs(deltaX) > swipeThreshold || Mathf.Abs(deltaY) > swipeThreshold)
                {
                    swipeDetected = true;
                    if (Mathf.Abs(deltaX) < Mathf.Abs(deltaY))
                    {
                        if (scrollRect == null)
                        {
                            scrollRect = FindObjectOfType<ScrollRect>();
                        }

                        if (scrollRect != null)
                        {
                            float normalizedDeltaY = deltaY / Screen.height;
                            scrollRect.verticalNormalizedPosition -= normalizedDeltaY;
                        }
                    }
                    else if (deltaX > 0)
                    {
                        LoadPreviousScene();
                    }
                }
            }
        }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                if (currentSceneIndex > 0)
                {
                    SceneManager.LoadScene(currentSceneIndex - 1);
                }
                else
                {
                    Application.Quit();
                }
            }
        

    }

    private void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;

        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }
}
