using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on obstacle.");
        }

        rb.isKinematic = true; // Препятствия движутся по скрипту
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Obstacle collided with player!");
            // Логика при столкновении с игроком
        }
    }

    public void ResetObstacle()
    {
        // Логика сброса состояния препятствия (при необходимости)
        Debug.Log("Obstacle has been reset.");
    }
}
