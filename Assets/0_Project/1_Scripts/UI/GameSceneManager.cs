using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    IEnumerator coroutine;

    void OnDestroy()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    public void SwitchScene(int buildIndex)
    {
        coroutine = loadScene(buildIndex);
        StartCoroutine(coroutine);
    }

    IEnumerator loadScene(int buildIndex)
    {
        yield return new WaitUntil(() => !AudioManager.instance.SFX_Source.isPlaying);
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
#endif
    }
}
