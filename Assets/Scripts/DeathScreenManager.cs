using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas; // Ссылка на Canvas для экрана смерти

    void Start()
    {
        // Скрываем Canvas при старте
        deathScreenCanvas.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        // Отображаем Canvas экрана смерти
        deathScreenCanvas.SetActive(true);
        // Останавливаем время в игре
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        // Перезапускаем уровень
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        // Переход в главное меню
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

