using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private DeathScreenManager deathScreenManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on player.");
        }

        // Найти DeathScreenManager
        deathScreenManager = GameObject.FindObjectOfType<DeathScreenManager>();
        if (deathScreenManager == null)
        {
            Debug.LogError("DeathScreenManager not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player collided with an obstacle!");
            if (deathScreenManager != null)
            {
                deathScreenManager.ShowDeathScreen();
            }
        }
    }

    public void ResetPlayer()
    {
        // Сброс состояния игрока
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log("Player has been reset.");
    }
}
