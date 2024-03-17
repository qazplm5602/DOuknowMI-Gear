using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates instance;
    [SerializeField] private RoomSpawner _roomSpawner;
    public Dictionary<OpeningDirection, GameObject[]> rooms = new();
    public Dictionary<Vector2, GameObject> posRoom = new();
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public Transform RoomParent;
    public int currentCount= 1;
    public int maxCount= 0;
    public bool generating = false;

    private void Awake()
    {
        instance = this;

        rooms[OpeningDirection.Up] = topRooms;
        rooms[OpeningDirection.Down] = bottomRooms;
        rooms[OpeningDirection.Left] = leftRooms;
        rooms[OpeningDirection.Right] = rightRooms;

        maxCount = 7;
    }    
}
