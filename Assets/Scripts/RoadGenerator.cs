using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadGenerator : MonoBehaviour
{
    public GameObject firstRoadPrefab; // Префаб первой дороги
    public List<GameObject> RoadPrefabs; // Список префабов остальных дорог
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    private float speed = 0;
    public int maxRoadCount = 10; // Увеличено количество дорог

    private float roadLength = 5.9f; // Длина одного участка дороги, измените это значение в зависимости от размера вашей дороги

    private PlayerSplashEffect playerSplashEffect;
    private ScoreManager scoreManager;

    void Start()
    {
        ResetLevel();
        // Получаем компонент PlayerSplashEffect от объекта игрока
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerSplashEffect = player.GetComponent<PlayerSplashEffect>();
        }

        // Получаем компонент ScoreManager
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager != null)
        {
            scoreManager = gameManager.GetComponent<ScoreManager>();
        }
    }

    void Update()
    {
        if (speed == 0) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        // Удаляем дорогу только когда она уходит на большее расстояние за игрока
        if (roads[0].transform.position.z < -5 * roadLength)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CreateNextRoad();
        }
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, roadLength);
        }

        GameObject roadPrefab;

        // Используем первый префаб для первой дороги
        if (roads.Count == 0)
        {
            roadPrefab = firstRoadPrefab;
        }
        else
        {
            // Выбираем случайный префаб для остальных дорог
            roadPrefab = RoadPrefabs[Random.Range(0, RoadPrefabs.Count)];
        }

        GameObject go = Instantiate(roadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void StartLevel()
    {
        speed = maxSpeed;
        // Запускаем эффект брызг
        if (playerSplashEffect != null)
        {
            playerSplashEffect.StartSplashEffect();
        }
        // Запускаем игру в ScoreManager
        if (scoreManager != null)
        {
            scoreManager.StartGame();
        }
    }

    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount + 7; i++)
        {
            CreateNextRoad();
        }

        // Останавливаем эффект брызг
        if (playerSplashEffect != null)
        {
            playerSplashEffect.StopSplashEffect();
        }

        // Сбрасываем игру в ScoreManager
        if (scoreManager != null)
        {
            scoreManager.ResetGame();
        }
    }

    public void IncreaseSpeed(float percentage)
    {
        speed += maxSpeed * percentage;
    }
}




