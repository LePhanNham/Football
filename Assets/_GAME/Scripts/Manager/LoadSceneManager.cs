using System;
using System.Collections;
using Base.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc phải có dòng này để làm việc với Scene

public class LoadSceneManager : MonoBehaviour
{
    [Header("Cài đặt UI")]
    [Tooltip("Kéo Panel màn hình đen hoặc Loading UI vào đây")]
    [SerializeField] private CanvasGroup loadingScreen;
    private void OnEnable()
    {
        loadingScreen.alpha = 0;
        loadingScreen.gameObject.SetActive(true);
        PlayerAction.OnReset += ReloadCurrentScene;
    }

    private void OnDisable()
    {
        PlayerAction.OnReset -= ReloadCurrentScene;
    }

    public void ReloadCurrentScene()
    {
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        if (loadingScreen != null) 
        {
            yield return loadingScreen.DOFade(1, 0.5f).WaitForCompletion();
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex);
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }
            if (loadingScreen != null)
            {
                yield return loadingScreen.DOFade(0, 0.7f).WaitForCompletion();
            }
        }
    }

}