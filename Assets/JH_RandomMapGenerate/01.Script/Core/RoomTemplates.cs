using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates instance;
    [SerializeField] private RoomSpawner _roomSpawner;

    public Dictionary<OpeningDirection, GameObject[]> rooms = new();
    public Dictionary<OpeningDirection, GameObject> LastRoom = new();
    public Dictionary<OpeningDirection, int> maxCountByDirection = new();
    public Dictionary<OpeningDirection, int> currentCountByDirection = new();

    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] LastRooms;

    public Transform roomParent;
    public int maxCount= 0;

    private void Awake()
    {
        instance = this;

        rooms[OpeningDirection.Up] = topRooms;
        rooms[OpeningDirection.Down] = bottomRooms;
        rooms[OpeningDirection.Left] = leftRooms;
        rooms[OpeningDirection.Right] = rightRooms;

        maxCountByDirection[OpeningDirection.Up] = (maxCount / 4) + Random.Range(-2, 3);
        maxCountByDirection[OpeningDirection.Down] = (maxCount / 4) + Random.Range(-2, 3);
        maxCountByDirection[OpeningDirection.Left] = (maxCount / 4) + Random.Range(-2, 3);
        maxCountByDirection[OpeningDirection.Right] = (maxCount / 4) + Random.Range(-2, 3);

        currentCountByDirection[OpeningDirection.Up] = 0;
        currentCountByDirection[OpeningDirection.Down] = 0;
        currentCountByDirection[OpeningDirection.Left] = 0;
        currentCountByDirection[OpeningDirection.Right] = 0;

        LastRoom[OpeningDirection.Up] = LastRooms[0];
        LastRoom[OpeningDirection.Down] = LastRooms[1];
        LastRoom[OpeningDirection.Left] = LastRooms[2];
        LastRoom[OpeningDirection.Right] = LastRooms[3];
    }    
}
