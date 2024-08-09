using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    public GameObject firstRoadPrefab; // Префаб первой дороги
    public List<GameObject> RoadPrefabs; // Список префабов остальных дорог
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    private float speed = 0;
    public int maxRoadCount = 10;

    private float roadLength = 5.9f; // Убедитесь, что переменная определена здесь

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

        // Проверьте, что roads[0] существует и доступен для проверки его позиции
        if (roads.Count > 0 && roads[0].transform.position.z < -5 * roadLength)
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

        GameObject roadPrefab = roads.Count == 0 ? firstRoadPrefab : RoadPrefabs[Random.Range(0, RoadPrefabs.Count)];
        GameObject go = Instantiate(roadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void StartLevel()
    {
        speed = maxSpeed;
        if (playerSplashEffect != null)
        {
            playerSplashEffect.StartSplashEffect();
        }
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

        if (playerSplashEffect != null)
        {
            playerSplashEffect.StopSplashEffect();
        }

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
