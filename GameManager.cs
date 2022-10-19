using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("Quit", 1) == 1)
        {
            PlayerPrefs.SetInt("Quit", 0);
            FindObjectOfType<WindowsController>().ShowMainMenu(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        PlayerPrefs.SetInt("Quit", 1);
        RestartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Quit", 1);
    }
}