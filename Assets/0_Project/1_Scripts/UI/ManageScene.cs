using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene: MonoBehaviour
{
    public void SwitchScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
