using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
            // Вызов метода ShowDeathScreen у deathScreenManager
            if (deathScreenManager != null)
            {
                deathScreenManager.ShowDeathScreen();
            }
        }
    }
}


