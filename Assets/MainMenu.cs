using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenItch()
    {
        Application.OpenURL("https://aeterponis.itch.io/");
    }

    public void OpenLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/in/burakreinh/");
    }

    public void OpenGit()
    {
        Application.OpenURL("https://github.com/reinhpash");
    }
    public void OpenPersonal()
    {
        Application.OpenURL("https://burakpiskin.netlify.app/");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int i)
    {
        PlayerPrefs.SetInt("level",i);
        SceneManager.LoadScene(1);
    }
}
