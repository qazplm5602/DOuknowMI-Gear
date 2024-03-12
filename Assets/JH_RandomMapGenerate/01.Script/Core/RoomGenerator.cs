using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Room))]
public class RoomGenerator : MonoBehaviour
{
    public enum GeneratorType
    {
        singularPath,
        multiPath,
        multiPathBalanced
    }

    public static bool useSeed = false;
    public static readonly int seed = 26;

    [SerializeField]
    int amountToGenerate = 32;

    [Range(0,2)]
    public int density;
    private GeneratorType type;

    public Room roomPrefab1x1;

    public bool ultraSpeed;
    public bool pathfindingExample = true;

    public static readonly float prefabsDistance = 1;
    public readonly Vector2[] offsets = new Vector2[]
    {
        Vector2.right * prefabsDistance,
        Vector2.left * prefabsDistance,
        Vector2.up * prefabsDistance,
        Vector2.down * prefabsDistance,
    };

    public List<Room> rooms;
    private List<Vector2> _roomsPos = new List<Vector2>();
    public MapGenerator _mapGenerator;
    private List<List<int>> roomChunks = new List<List<int>>();
    private int chunkWidth = 8;

    private Transform roomsContainer;

    private bool creatingPool = true;
    [HideInInspector]
    public bool generatingRooms;
    [HideInInspector]
    public bool generatingStructure = true;
    [HideInInspector]
    public Room generatorRoom;
    private Vector2 generatorPosition;

    private void Awake()
    {
        if (useSeed)
            Random.InitState(seed);

        type = (GeneratorType)density;

        rooms = new List<Room>();
        generatorRoom = GetComponent<Room>();
        generatorRoom.jumpsFromStart = 0;
        generatorPosition = transform.position;
        generatingRooms = true;
        _mapGenerator = GetComponent<MapGenerator>();

        roomsContainer = new GameObject("Rooms").transform;
    }

    void Start()
    {
        StartCoroutine(MakeRoom());
    }

    public IEnumerator MakeRoom()
    {
        //pooling
        StartCoroutine(CreatePool(roomPrefab1x1));
        while (creatingPool)
            yield return new WaitForSeconds(0.05f);

        //placing
        if (type == GeneratorType.singularPath)
            StartCoroutine(PlaceRooms());
        else
            StartCoroutine(PlaceRoomsMultiPath());
        while (generatingRooms)
            yield return new WaitForSeconds(0.05f);

        //features
        GenerateDoors();

        if (pathfindingExample)
        {
            List<Room> roomsSortedByDist = PathManager.SortForPathFinding(generatorRoom, amountToGenerate + 1);
            PathManager.AssignJumps(roomsSortedByDist);
            FurthestRoomActions();
        }

        generatingStructure = false;
        roomChunks.Clear();
    }

    public IEnumerator CreatePool(Room prefab)
    {
        Vector3 position = transform.position;
        for (int i = 0; i < amountToGenerate; i++)
        {
            if (i % 500 == 0) yield return null; //reduce lag

            Room newRoom = Instantiate(prefab, position, Quaternion.identity, roomsContainer);
            newRoom.gameObject.name = "Room " + i;
            newRoom.SetRandomBodyColor();
            rooms.Add(newRoom);
        }
        //_mapGenerator.SpawnMaps(rooms);
        yield return null;
        creatingPool = false;
    }

    //singular path, multi direction sitting
    private IEnumerator PlaceRooms()
    {
        generatingRooms = true;
        Room.Directions dir;
        Vector2 offset;
        Vector2 last = transform.position;
        int roomsCorrectlySet = 0;

        for (int i = 0; i < amountToGenerate; i++)
        {
            if (ultraSpeed && i % 25 == 0)
                yield return null;

            dir = RandomDirection();

            offset = offsets[(int)dir];
            Vector2 newRoomPos = last + offset;
            _roomsPos.Add(newRoomPos);
            List<int> selectedChunk;
            Room newRoom;
            PlaceRoom(roomsCorrectlySet, newRoomPos, out selectedChunk, out newRoom);

            bool collision;
            //yield return null;
            if (ultraSpeed)
                collision = newRoom.IsCollidingForPooled(selectedChunk, rooms, generatorPosition);
            else
            {
                yield return new WaitForFixedUpdate();      //best performance
                //yield return new WaitForSeconds(0.1f);    //animated look
                collision = newRoom.collision;
                newRoom.collision = false;
            }

            last = newRoomPos;
            if (collision)
            {
                i--;
                continue;
            }
            selectedChunk.Add(roomsCorrectlySet);
            roomsCorrectlySet++;
        }
        print("Á¦¹ß");
       
        yield return null;
        generatingRooms = false;
    }

    //multi path, multi direction sitting
    private IEnumerator PlaceRoomsMultiPath()
    {
        Vector2[] lastPosition = new Vector2[PathManager.GENERATOR_PATHS_AMOUNT];
        int roomsCorrectlySet = 0;
        List<int> selectedChunk;

        Room SelectRoom(int pathIndex)
        {
            Room.Directions dir = RandomDirection();

            Vector2 offset = offsets[(int)dir];
            Vector2 newRoomPos = lastPosition[pathIndex] + offset;
            lastPosition[pathIndex] = newRoomPos;

            Room newRoom;
            PlaceRoom(roomsCorrectlySet,  newRoomPos, out selectedChunk, out newRoom);

            return newRoom;
        }

        generatingRooms = true;
        if (!roomsContainer)
            roomsContainer = new GameObject("Rooms").transform;

        //set random path sizes
        int[] pathRooms = PathManager.GeneratorPathRoomsAmount(amountToGenerate, type == GeneratorType.multiPathBalanced);
        int pathCalls = Mathf.Max(pathRooms);

        //generate paths
        for (int i = 1; i <= pathCalls; i++)
        {
            //partition ultra speed generation
            if (ultraSpeed && i % 25 == 0)
                yield return null;

            //generate in cycles: one room per path
            //1st iteration creates 1st room for each path (generator's neighbours)
            for (int pathIndex = 0; pathIndex < PathManager.GENERATOR_PATHS_AMOUNT; pathIndex++)
            {
                //skip if no rooms for this path
                if (i > pathRooms[pathIndex])
                    continue;

                Room newRoom = SelectRoom(pathIndex);
                bool collision;
                //yield return null;
                if (ultraSpeed)
                    collision = newRoom.IsCollidingForPooled(selectedChunk, rooms, generatorPosition);
                else
                {
                    yield return new WaitForFixedUpdate();      //best performance
                    //yield return new WaitForSeconds(0.1f);    //animated look
                    collision = newRoom.collision;
                    newRoom.collision = false;
                }  

                if (collision)
                {
                    pathIndex--;
                    continue;
                }
                selectedChunk.Add(roomsCorrectlySet);
                roomsCorrectlySet++;
            }
        }

        yield return null;
        generatingRooms = false;
    }

    private void PlaceRoom(int roomsCorrectlySet, Vector2 newRoomPos, out List<int> selectedChunk, out Room newRoom)
    {
        selectedChunk = SelectChunk(newRoomPos);

        newRoom = rooms[roomsCorrectlySet];
        newRoom.transform.position = newRoomPos;
        newRoom.gameObject.SetActive(true);
    }

    private void GenerateDoors()
    {
        generatorRoom.AssignAllNeighbours(offsets);

        for (int i = 0; i < amountToGenerate; i++)
        {
            rooms[i].AssignAllNeighbours(offsets);
        }
    }
    private void FurthestRoomActions()
    {
        Room furthest = PathManager.FindFurthestRoom(rooms);
        if (furthest != null)
        {
            furthest.MarkAsBossRoom();
            PathManager.SetPathToRoom(furthest,generatorRoom);
        }
        else
            Debug.LogError("FindFurthestRoom() returned null - cannot set path.");
    }

    private Room.Directions RandomDirection()
    {
        return (Room.Directions)Random.Range(0, 4);
    }

    private List<int> SelectChunk(Vector2 newRoomPos)
    {
        List<int> selectedChunk;
        int xDist = (int)Mathf.Abs(newRoomPos.x - generatorPosition.x);
        int chunkIndex = xDist / chunkWidth;
        while (chunkIndex >= roomChunks.Count)
            roomChunks.Add(new List<int>());
        selectedChunk = roomChunks[chunkIndex];
        return selectedChunk;
    }

    //based on physical distance
    //private Room FindFurthestRoom()
    //{
    //    int index = -1;
    //    float biggestDist = 0;
    //    for (int i = 0; i < rooms.Count; i++)
    //    {
    //        float dist = (transform.position - rooms[i].transform.position).sqrMagnitude;
    //        if (dist > biggestDist)
    //        {
    //            index = i;
    //            biggestDist = dist;
    //        }
    //    }
    //    if (index != -1)
    //        return rooms[index];
    //    else
    //        return null;
    //}
}
