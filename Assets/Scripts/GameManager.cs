using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 lastPlayerPosition;
    private float lastScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerState(Vector3 position, float score)
    {
        lastPlayerPosition = position;
        lastScore = score;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        ShowGameOverScreen();
    }

    private void ShowGameOverScreen()
    {
        GameObject gameOverScreen = GameObject.Find("GameOverScreen");
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RespawnPlayer()
    {
        Time.timeScale = 1f;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = lastPlayerPosition;
            player.GetComponent<Player>().Respawn();
            SwipeManager.instance.SetPlayer(player.transform);
        }

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.SetScore(lastScore);
        }

        GameObject gameOverScreen = GameObject.Find("GameOverScreen");
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }
}
