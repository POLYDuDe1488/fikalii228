using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

        rb.isKinematic = true; // ����������� �������� �� �������
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Obstacle collided with player!");
            // ����� ������ ������ ������ ��� ��������� ����
            // ��������, gameManager.GameOver();
        }
    }
}
