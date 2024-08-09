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
        // ������������� ������� � ��������� ������
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
            initialPlayerRotation = player.transform.rotation;
            playerRigidbody = player.GetComponent<Rigidbody>();
        }

        // �������� ������ �� DeathScreenManager
        deathScreenManager = FindObjectOfType<DeathScreenManager>();
    }

    public void RestartGame()
    {
        ResetLevel();
        ResetGameObjects();

        // �������� ����� ������ ����� ��������
        if (deathScreenManager != null && deathScreenManager.deathScreenCanvas != null)
        {
            deathScreenManager.deathScreenCanvas.SetActive(false);
        }
    }

    private void ResetLevel()
    {
        Time.timeScale = 1f;

        // �������������� ������� ������ � ��� ����������
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

        // ����� ������ ��������, ���� ����������
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
        // �����, ������� �������� �� ����� ��������� ���� �������� � ����
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
