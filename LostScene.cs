using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostScene : MonoBehaviour
{
    public GameObject YouLostMenu;
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void TryAgain()
    {
        YouLostMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
