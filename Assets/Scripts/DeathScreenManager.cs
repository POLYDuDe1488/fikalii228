using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas; // ������ �� Canvas ��� ������ ������

    void Start()
    {
        // �������� Canvas ��� ������
        deathScreenCanvas.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        // ���������� Canvas ������ ������
        deathScreenCanvas.SetActive(true);
        // ������������� ����� � ����
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        // ������������� �������
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        // ������� � ������� ����
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

