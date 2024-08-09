using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // ������ �� ��������� ������� ��� ����������� �����
    public Text highScoreText; // ������ �� ��������� ������� ��� ����������� �������
    private float score; // ���������� ��� �������� ������� �����
    public float pointsPerSecond = 10f; // ���������� �����, ����������� ������ �������

    public RoadGenerator roadGenerator; // ������ �� RoadGenerator
    public Material skybox500; // �������� ��� 500 �����
    public Material skybox1000; // �������� ��� 1000 �����
    public Material skybox1500;

    private bool isSkybox500Changed = false; // ���� ��� �������� ��������� ��������� �� 500 �����
    private bool isSkybox1000Changed = false; // ���� ��� �������� ��������� ��������� �� 1000 �����
    private bool isSkybox1500Changed = false;
    private bool isGameRunning = false; // ���� ��� �������� ���������� ����

    private int highScore; // ���������� ��� �������� �������

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the inspector!");
            return;
        }

        // ��������� ������
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();

        score = 0f; // ������������� �����
        UpdateScoreText(); // ��������� ����� ����� � ������ ����
    }

    void Update()
    {
        if (scoreText == null || !isGameRunning) return;

        // ����������� ���� � ����������� �� �������
        score += pointsPerSecond * Time.deltaTime;
        UpdateScoreText();

        // ��������� ���������� 500 ����� � ������ ��������
        if (score >= 500 && !isSkybox500Changed)
        {
            ChangeSkybox(skybox500);
            IncreaseSpeed();
            isSkybox500Changed = true;
        }

        // ��������� ���������� 1000 ����� � ������ ��������
        if (score >= 1000 && !isSkybox1000Changed)
        {
            ChangeSkybox(skybox1000);
            IncreaseSpeed();
            isSkybox1000Changed = true;
        }

        if (score >= 1500 && !isSkybox1500Changed)
        {
            ChangeSkybox(skybox1500);
            IncreaseSpeed();
            isSkybox1500Changed = true;
        }
    }
    public void SetScore(float newScore)
    {
        score = newScore;
        UpdateScoreText(); // �������� ����� �� ������, ���� ��� ����������
    }

    void UpdateScoreText()
    {
        // ��������� ����� �����
        scoreText.text = Mathf.FloorToInt(score).ToString();

        // ��������� � ��������� ������
        if (Mathf.FloorToInt(score) > highScore)
        {
            highScore = Mathf.FloorToInt(score);
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "������: " + highScore.ToString();
        }
    }

    void ChangeSkybox(Material newSkybox)
    {
        RenderSettings.skybox = newSkybox;
        DynamicGI.UpdateEnvironment();
    }

    void IncreaseSpeed()
    {
        if (roadGenerator != null)
        {
            roadGenerator.IncreaseSpeed(0.15f); // ����������� �������� �� 15%
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
    }

    public void ResetGame()
    {
        score = 0;
        isSkybox500Changed = false;
        isSkybox1000Changed = false;
        isSkybox1500Changed = false;
        isGameRunning = false;
        UpdateScoreText();
    }
}




