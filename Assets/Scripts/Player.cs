using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private DeathScreenManager deathScreenManager;
    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on player.");
        }

        // ����� DeathScreenManager
        deathScreenManager = GameObject.FindObjectOfType<DeathScreenManager>();
        if (deathScreenManager == null)
        {
            Debug.LogError("DeathScreenManager not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isInvincible)
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
        // ����� ��������� ������
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log("Player has been reset.");
    }

    public void Respawn()
    {
        // ��������������� ��������� ��������� ������
        transform.position = Vector3.zero;  // ��� �������, ��� ������ ������������ �����
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // ����� �������� ������
            rb.angularVelocity = Vector3.zero;  // ����� ������� ��������
        }
        Debug.Log("Player respawned at position: " + transform.position);
    }

    public void StartInvincibility(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}