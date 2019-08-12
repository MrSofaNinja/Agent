using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // String for the scene name 
    public string SceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadLocationMap()
    {
        SceneManager.LoadScene("AgentGameScreen");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScreen");
    }

    public void LoadTrafficMap()
    {
        SceneManager.LoadScene("AgentTrafficTestScene");
    }

    public void LoadDebriefScreen()
    {
        SceneManager.LoadScene("OpDebriefScreen");
    }
}
