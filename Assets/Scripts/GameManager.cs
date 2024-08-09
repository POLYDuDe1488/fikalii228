using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    public float moveSpeed = 10f; // —корость перемещени€ игрока
    public Transform player; // —сылка на игрока

    public float minX = -2.159955f; // ћинимальное значение по оси X
    public float maxX = 1.772743f;  // ћаксимальное значение по оси X

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (player != null && Input.GetMouseButton(0))
        {
            MovePlayerTowardsCursor();
        }
    }

    private void MovePlayerTowardsCursor()
    {
        if (player == null) return;

        // ѕолучаем позицию курсора по оси X
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, 0, Camera.main.transform.position.y));

        // »гнорируем перемещение по вертикали и оси Z
        float targetX = Mathf.Clamp(mousePosition.x, minX, maxX);

        // ѕеремещаем игрока в направлении курсора по оси X
        Vector3 newPosition = new Vector3(targetX, player.position.y, player.position.z);
        player.position = Vector3.MoveTowards(player.position, newPosition, moveSpeed * Time.deltaTime);
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
        // ”станавливаем позицию игрока в центр по оси X при установке нового игрока
        player.position = new Vector3(0, player.position.y, player.position.z);
    }
}

