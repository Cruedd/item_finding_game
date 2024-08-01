using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Demo_01");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToScoreMenu()
    {

        Debug.Log("Score menu yapýlmadý");
    }

    public void QuitGame()
    {
        Debug.Log("Çýkýþ yapýlýyor");
        Application.Quit();
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
