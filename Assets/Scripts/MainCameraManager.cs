using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCameraManager : MonoBehaviour
{
    private Vector3 initialPlayerPosition;
    private Quaternion initialPlayerRotation;
    private Rigidbody playerRigidbody;
    private DeathScreenManager deathScreenManager;

    private void Start()
    {
        // Инициализация позиции и состояния игрока
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
            initialPlayerRotation = player.transform.rotation;
            playerRigidbody = player.GetComponent<Rigidbody>();
        }

        // Получаем ссылку на DeathScreenManager
        deathScreenManager = FindObjectOfType<DeathScreenManager>();
    }

    public void RestartGame()
    {
        ResetLevel();
        ResetGameObjects();

        // Скрываем экран смерти после рестарта
        if (deathScreenManager != null && deathScreenManager.deathScreenCanvas != null)
        {
            deathScreenManager.deathScreenCanvas.SetActive(false);
        }
    }

    private void ResetLevel()
    {
        Time.timeScale = 1f;

        // Восстановление позиции игрока и его параметров
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = initialPlayerPosition;
            player.transform.rotation = initialPlayerRotation;

            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }

            Debug.Log("Player position and state reset.");
        }

        // Сброс других объектов, если необходимо
        RoadGenerator roadGenerator = FindObjectOfType<RoadGenerator>();
        if (roadGenerator != null)
        {
            roadGenerator.ResetLevel();
        }

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetGame();
        }

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.RestartGame();
        }
    }

    private void ResetGameObjects()
    {
        // Метод, который отвечает за сброс состояния всех объектов в игре
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ResetPlayer();
        }

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.ResetObstacle();
        }
    }
}
