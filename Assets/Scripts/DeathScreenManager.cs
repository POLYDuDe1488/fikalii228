using UnityEngine;
using UnityEngine.SceneManagement;  // Подключение библиотеки для работы с SceneManager
using UnityEngine.UI;  // Подключение библиотеки для работы с UI

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas;
    public Button respawnButton; // Кнопка возрождения

    void Start()
    {
        deathScreenCanvas.SetActive(false);

        if (respawnButton != null)
        {
            respawnButton.onClick.AddListener(OnRespawnButtonClicked);
        }
        else
        {
            Debug.LogWarning("Respawn Button is not assigned in the inspector!");
        }
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

    private void OnRespawnButtonClicked()
    {
        GameManager.Instance.RespawnPlayer(); // Вызов метода возрождения в GameManager
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

