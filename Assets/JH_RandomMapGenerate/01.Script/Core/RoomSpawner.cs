using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum OpeningDirection
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
}
// 1 - > need up Door;
// 2 -> need down Door;
// 3 -> need left Door;
// 4 -> need right Door;
public class RoomSpawner : MonoBehaviour
{
    public OpeningDirection openingDirection = OpeningDirection.Up;

    private bool _spawned = false;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private GameObject _wallPrefab;
    private void Start()
    {
        if (RoomTemplates.instance.maxCount <= RoomTemplates.instance.currentCount) return;
        ++RoomTemplates.instance.currentCount;
        Invoke(nameof(SpawnRoom), 0.1f);
    }
    //private void MakeLastRoom()
    //{
    //    if (_spawned) print("이거왜쓰는지모르겠는데?");
    //    Instantiate(RoomTemplates.instance.LastRoom[openingDirection], transform.position, Quaternion.identity, RoomTemplates.instance.roomParent);
    //    _spawned = true;
    //}
    public void SpawnRoom()
    {
        if (_spawned) return;
        int _rand = Random.Range(0, 6);
        GameObject[] _rooms = RoomTemplates.instance.rooms[openingDirection];
        Instantiate(_rooms[_rand], transform.position, Quaternion.identity, RoomTemplates.instance.roomParent);

        _spawned = true;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomSpawner>() == null)
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomSpawner>()._spawned == true)
        {
            Destroy(gameObject);
        }

    }
}
