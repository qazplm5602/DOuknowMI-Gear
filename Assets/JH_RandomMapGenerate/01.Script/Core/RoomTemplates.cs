using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates instance;
    [SerializeField] private RoomSpawner _roomSpawner;

    public Dictionary<OpeningDirection, GameObject[]> rooms = new();

    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public Transform roomParent;
    public int maxCount= 0;
    public int currentCount = 0;

    private void Awake()
    {
        instance = this;

        rooms[OpeningDirection.Up] = topRooms;
        rooms[OpeningDirection.Down] = bottomRooms;
        rooms[OpeningDirection.Left] = leftRooms;
        rooms[OpeningDirection.Right] = rightRooms;
    }    
}
