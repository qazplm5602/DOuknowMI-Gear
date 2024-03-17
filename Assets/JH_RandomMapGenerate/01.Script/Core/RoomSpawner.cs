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
    [SerializeField] private GameObject fuck;
    private void Start()
    {
        if (RoomTemplates.instance.currentCount >= RoomTemplates.instance.maxCount)
        {
            RoomTemplates.instance.generating = true;
            return;
        }
            RoomTemplates.instance.currentCount++;
        Invoke(nameof(SpawnRoom), 0.1f);
    }

    public void SpawnRoom()
    {
        if (_spawned) return;

        int _rand = Random.Range(0, 8);
        GameObject[] _rooms = RoomTemplates.instance.rooms[openingDirection];
        Instantiate(_rooms[_rand], transform.position, Quaternion.identity, RoomTemplates.instance.RoomParent);
        _spawned = true;
    }

    IEnumerator GenerateEnd()
    {
        yield return new WaitUntil(() => RoomTemplates.instance.generating);
        Vector2 dir = transform.parent.position - transform.position;
        dir.Normalize();
        MakeWall(dir);
    }

    void MakeWall(Vector2 direction)
    {
        print("±Â");
        Instantiate(fuck, direction * _offset, Quaternion.identity);
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
