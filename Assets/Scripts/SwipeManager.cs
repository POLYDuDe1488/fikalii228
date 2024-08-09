using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    public float moveSpeed = 10f; // �������� ����������� ������
    public float turnSpeed = 5f; // �������� �������� ������
    public Transform player; // ������ �� ������

    public float minX = -2.159955f; // ����������� �������� �� ��� X
    public float maxX = 1.772743f;  // ������������ �������� �� ��� X

    private Vector3 lastMousePosition = Vector3.zero;

    Vector2 TouchPosition() { return (Vector2)Input.mousePosition; }
    bool TouchBegan() { return Input.GetMouseButtonDown(0); }
    bool TouchEnded() { return Input.GetMouseButtonUp(0); }
    bool GetTouch() { return Input.GetMouseButton(0); }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (player != null && GetTouch())
        {
            MovePlayerTowardsCursor();
        }
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
        // ������������� ������� ������ � ����� �� ��� X ��� ��������� ������ ������
        player.position = new Vector3(0, player.position.y, player.position.z);
    }

    public void ResetSwipe()
    {
        lastMousePosition = Vector3.zero;
    }

    private void MovePlayerTowardsCursor()
    {
        if (player == null) return;

        Vector3 mousePosition = GetWorldPositionOnPlane(Input.mousePosition, player.position.y);
        Vector3 direction = (mousePosition - player.position);
        direction.y = 0; // ���������� ����������� �� ���������
        direction.z = 0; // ���������� ����������� ������ � �����

        // ���������� ������ � ����������� �������
        Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
        if (Mathf.Abs(mousePosition.x - player.position.x) < Mathf.Abs(moveVector.x))
        {
            // ��������������� �� ����� �������
            player.position = new Vector3(Mathf.Clamp(mousePosition.x, minX, maxX), player.position.y, player.position.z);
        }
        else
        {
            float newX = Mathf.Clamp(player.position.x + moveVector.x, minX, maxX);
            player.position = new Vector3(newX, player.position.y, player.position.z);
        }

        // ������� ������� ������ � ����������� �������� � ������ ������� ������ �����
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up); // ������� ������ ������
            player.rotation = Quaternion.Lerp(player.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, y, 0));
        groundPlane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}
