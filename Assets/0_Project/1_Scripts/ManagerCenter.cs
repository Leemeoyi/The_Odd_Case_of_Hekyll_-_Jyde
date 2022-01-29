using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCenter : MonoBehaviour
{
    public static GameSceneManager sceneManager;

    void Awake()
    {
        GameSceneManager gameSceneManager = GameObject.FindObjectOfType<GameSceneManager>()
            ?.GetComponent<GameSceneManager>();
        if (gameSceneManager)
        {
            sceneManager = gameSceneManager;
        }
        else
        {
            sceneManager = gameObject.AddComponent<GameSceneManager>();
        }
    }

    public void SwitchScene(int buildIndex)
    {
        sceneManager.SwitchScene(buildIndex);
    }

    public void QuitGame()
    {
        sceneManager.QuitGame();
    }
}
