using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagementScene : MonoBehaviour
{
    public GameObject menus;

    bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        menus.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Pause()
    {
        menus.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Hub");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jeu quitté.");
    }
}
