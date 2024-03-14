using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private Vector2 _offset = new Vector2(500, 500);
    [SerializeField] private GameObject[] _roomPrefabs;

    public void SpawnMaps(List<Vector2> _roomsPos)
    {
        for(int i = 0; i < _roomsPos.Count; i++)
        {
            int randIdx = Random.Range(0, _roomPrefabs.Length);
            Instantiate(_roomPrefabs[randIdx],
                RoomPos(i, _roomsPos),
                Quaternion.identity);
        }
       
    }

    Vector2 RoomPos(int roomsIdx, List<Vector2> _rooms)
    {
        print(roomsIdx);
        print(_rooms[roomsIdx]);
        print("---------------------");
        float xPos = _rooms[roomsIdx].x * _offset.x;
        float yPos = _rooms[roomsIdx].y * _offset.y;
        Vector2 pos = new Vector2(xPos, yPos);
        return pos;
    }
}
