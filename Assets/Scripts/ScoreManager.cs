using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Ссылка на текстовый элемент для отображения очков
    public Text highScoreText; // Ссылка на текстовый элемент для отображения рекорда
    private float score; // Переменная для хранения текущих очков
    public float pointsPerSecond = 10f; // Количество очков, добавляемых каждую секунду

    public RoadGenerator roadGenerator; // Ссылка на RoadGenerator
    public Material skybox500; // Скайбокс для 500 очков
    public Material skybox1000; // Скайбокс для 1000 очков
    public Material skybox1500;

    private bool isSkybox500Changed = false; // Флаг для проверки изменения скайбокса на 500 очков
    private bool isSkybox1000Changed = false; // Флаг для проверки изменения скайбокса на 1000 очков
    private bool isSkybox1500Changed = false;
    private bool isGameRunning = false; // Флаг для проверки запущенной игры

    private int highScore; // Переменная для хранения рекорда

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the inspector!");
            return;
        }

        // Загружаем рекорд
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();

        score = 0f; // Инициализация очков
        UpdateScoreText(); // Обновляем текст очков в начале игры
    }

    void Update()
    {
        if (scoreText == null || !isGameRunning) return;

        // Увеличиваем очки в зависимости от времени
        score += pointsPerSecond * Time.deltaTime;
        UpdateScoreText();

        // Проверяем достижение 500 очков и меняем скайбокс
        if (score >= 500 && !isSkybox500Changed)
        {
            ChangeSkybox(skybox500);
            IncreaseSpeed();
            isSkybox500Changed = true;
        }

        // Проверяем достижение 1000 очков и меняем скайбокс
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
        UpdateScoreText(); // Обновите текст на экране, если это необходимо
    }

    void UpdateScoreText()
    {
        // Обновляем текст очков
        scoreText.text = Mathf.FloorToInt(score).ToString();

        // Проверяем и обновляем рекорд
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
            highScoreText.text = "Рекорд: " + highScore.ToString();
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
            roadGenerator.IncreaseSpeed(0.15f); // Увеличиваем скорость на 15%
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




