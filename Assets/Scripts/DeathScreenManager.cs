using UnityEngine;
using UnityEngine.SceneManagement;  // Подключение библиотеки для работы с SceneManager

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas;

    void Start()
    {
        deathScreenCanvas.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        if (deathScreenCanvas != null)
        {
            deathScreenCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("DeathScreenCanvas has been destroyed or is not assigned.");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        MainCameraManager cameraManager = FindObjectOfType<MainCameraManager>();
        if (cameraManager != null)
        {
            cameraManager.RestartGame();
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
