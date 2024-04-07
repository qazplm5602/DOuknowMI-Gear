using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mapParent;
    public enum DIRECTION { NODATA = -1, UP, RIGHT, DOWN, LEFT, MAX };

    public Dictionary<DIRECTION, Vector2Int> dirIndex = new Dictionary<DIRECTION, Vector2Int>()
    {
        { DIRECTION.UP,new Vector2Int(0,1)},
        { DIRECTION.RIGHT,new Vector2Int(1,0)},
        { DIRECTION.DOWN,new Vector2Int(0,-1)},
        { DIRECTION.LEFT,new Vector2Int(-1,0)},
    };

    public Map map;

    [Serializable]
    public struct SpawnData
    {
        public int MaxCnt;
        public int MinCnt;

        public int MaxListSize;

        public int MaxStatue;
    }

    [Serializable]
    public class CurrentValue
    {
        public int NowCount;
        [SerializeField]
        public StageData[] Maplist;
        public List<GameObject> CurrentRoomObjs;
        public List<int> PrayRoomCandidates; // 조각상 방으로 가능한 방들의 인덱스 목록
        public GameObject[] MapObjList;
        public BaseStage StartRoom;

        public int LargeRoomChance;
        public int MediumRoomChance;

        public int StatueCount;
    }

    public SpawnData jajiOption = new SpawnData();
    public CurrentValue current = new CurrentValue();
    public void InitSetting()
    {
        current.PrayRoomCandidates = new List<int>();
        current.Maplist = new StageData[jajiOption.MaxListSize * jajiOption.MaxListSize];
        current.MapObjList = new GameObject[jajiOption.MaxListSize * jajiOption.MaxListSize];
        current.CurrentRoomObjs = new List<GameObject>();
        for (int i = 0; i < jajiOption.MaxListSize * jajiOption.MaxListSize - 1; i++)
        {
            current.Maplist[i] = null;
            current.MapObjList[i] = null;
        }
    }


    private void Start()
    {
        InitSetting();
        MapSpawn(0, 2, null, 0);
        MakeMapMotherfuker();
        MakeStatueRoom();
        ShowMaps();
    }

    public Vector2Int MapSpawn(int x, int y, StageData Parent, int depth)
    {
        //맵이 최대 개수까지 만들어 지면 종료한다.
        if (current.NowCount >= jajiOption.MaxCnt)
        {
            return new Vector2Int(x, y);
        }

        int RandNum;
        int yval = jajiOption.MaxListSize;

        //들어오면 해당 위치에 방을 만들고 
        //음식점, 상점, 방 크기, 상자 스폰 등을 결정한다.
        //보스전이 있어야 하면 보스방도 스폰
        if (current.Maplist[x + (y * yval)] == null)
        {
            current.Maplist[x + (y * yval)] = new StageData();
            current.Maplist[x + (y * yval)].InitSttting(current.NowCount, x, y);
            current.NowCount++;

            //각 방들은 서로 연결되어서 이동할 수 있어야 하지만 인접해 있다고 항상 연결되어 있어야 하는것은 아니다
            //따라서 탐색을 할때 자신이 현재 탐색한 위치에서 이전에 있었던 위치를 넘겨줌으로써
            //탐색을 수행한 경로가 방을 이어주는 통로가 될 수 있도록 해준다.
            if (Parent != null)
            {
                if (Parent.indexX == x)
                {
                    if (Parent.indexY > y)//아래쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].DownMap = Parent;
                        Parent.UpMap = current.Maplist[x + (y * yval)];
                    }
                    else//위쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].UpMap = Parent;
                        Parent.DownMap = current.Maplist[x + (y * yval)];


                    }
                }
                else if (Parent.indexY == y)
                {
                    if (Parent.indexX > x)//오른쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].RightMap = Parent;
                        Parent.LeftMap = current.Maplist[x + (y * yval)];
                    }
                    else//왼쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].LeftMap = Parent;
                        Parent.RightMap = current.Maplist[x + (y * yval)];
                    }
                }
            }

        }

        //천번째 방은 항상 오른쪽으로간다.
        if (current.NowCount == 1)
        {
            MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
            //return jajiOption.MaxNum;
            return new Vector2Int(x + 1, y);
        }

        //왼쪽
        RandNum = Random.Range(0, 100);
        //Debug.Log($"랜덤{RandNum}");
        if (RandNum <= 50)
        {
            if (x - 1 >= 1 && current.Maplist[(x - 1) + (y * yval)] == null)
            {
                MapSpawn(x - 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //오른쪽
        RandNum = Random.Range(0, 100);

        if (RandNum <= 70)
        {
            if (x + 1 <= jajiOption.MaxListSize - 1 && current.Maplist[(x + 1) + (y * yval)] == null)
            {
                MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //위쪽
        RandNum = Random.Range(0, 100);
        if (RandNum <= 35)
        {
            if (y - 1 >= 0 && current.Maplist[x + ((y - 1) * yval)] == null)
            {
                MapSpawn(x, y - 1, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //아래쪽
        RandNum = Random.Range(0, 100);

        if (RandNum <= 35)
        {
            if (y + 1 <= jajiOption.MaxListSize - 1 && current.Maplist[x + ((y + 1) * yval)] == null)
            {
                MapSpawn(x, y + 1, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //이렇게 확률로 움직이도록 하면 최소개수가 만들어 지지 않을수 있기 때문에 최소 개수가 채워지지 않으면 4 방향중 비어있는 곳을 찾아서 강제로 생성시켜 줍니다.
        if (current.NowCount < jajiOption.MinCnt)
        {
            for (int i = 3; i >= 0; i++)
            {
                int tempx = x + dirIndex[(DIRECTION)i].x;
                int tempy = y + dirIndex[(DIRECTION)i].y;
                if (tempx >= 0 && tempx < jajiOption.MaxListSize - 1 && tempy >= 0 && tempy < jajiOption.MaxListSize - 1)
                {
                    if (current.Maplist[tempx + ((tempy + 1) * yval)] == null)
                    {
                        MapSpawn(tempx, tempy, current.Maplist[(tempx) + (tempy * yval)], depth + 1);
                    }
                }
            }
        }

        return new Vector2Int(x, y);
    }

    public void ShowMaps()
    {
        int interval = 30;
        int yval = jajiOption.MaxListSize;

        for (int y = 0; y < jajiOption.MaxListSize; y++)
        {
            for (int x = 0; x < jajiOption.MaxListSize; x++)
            {
                if (current.MapObjList[x + (y * yval)] != null)
                {
                    GameObject obj = current.MapObjList[x + (y * yval)];
                    BaseStage stageData = obj.GetComponent<BaseStage>();
                    obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));

                    int num = stageData.StageLinkedData.Num;
                    obj.name = $"Room_{num}";

                    stageData.Init();
                    stageData.StageNum = num;

                    if (stageData.type == ROOMTYPE.Start)
                    {
                        Debug.Log(stageData.type + stageData.transform.name);
                        current.StartRoom = stageData;
                    }
                }
            }
        }

    }

    public LinkedStage SetLinkingData(int index)
    {
        LinkedStage linkeddata = new LinkedStage();
        int yval = jajiOption.MaxListSize;
        int x = index % yval;
        int y = index / yval;
        linkeddata.Num = current.Maplist[index].Num;

        //check linked rooms, and if exist, link their doors actually

        if (current.Maplist[index].LeftMap != null)
        {
            var mapObj = current.MapObjList[(x - 1) + (y * yval)];
            if (mapObj != null)
            {
                linkeddata.LeftMap = mapObj;
                if (mapObj.GetComponent<BaseStage>().StageLinkedData != null)
                {
                    mapObj.GetComponent<BaseStage>().StageLinkedData.RightMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if (current.Maplist[index].RightMap != null)
        {
            var mapObj = current.MapObjList[(x + 1) + (y * yval)];
            if (mapObj != null)
            {
                linkeddata.RightMap = mapObj;

                if (mapObj.GetComponent<BaseStage>().StageLinkedData != null)
                {
                    mapObj.GetComponent<BaseStage>().StageLinkedData.LeftMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if (current.Maplist[index].UpMap != null)
        {
            var mapObj = current.MapObjList[x + ((y - 1) * yval)];
            if (mapObj != null)
            {
                linkeddata.UpMap = mapObj;

                if (mapObj.GetComponent<BaseStage>().StageLinkedData != null)
                {
                    mapObj.GetComponent<BaseStage>().StageLinkedData.DownMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if (current.Maplist[index].DownMap != null)
        {
            var mapObj = current.MapObjList[x + ((y + 1) * yval)];
            if (mapObj != null)
            {
                linkeddata.DownMap = mapObj;

                if (mapObj.GetComponent<BaseStage>().StageLinkedData != null)
                {
                    mapObj.GetComponent<BaseStage>().StageLinkedData.UpMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }

        return linkeddata;
    }

    public void MakeMapMotherfuker()
    {
        bool[] dir = new bool[(int)Door.DoorType.DoorMax];

        int size = jajiOption.MaxListSize;
        for (int i = 0; i < size * size; i++)
        {
            int countDoor = 0;
            bool flag = false;

            for (int b = 0; b < dir.Length; b++)
            {
                dir[b] = false;
            }


            if (current.Maplist[i] != null)
            {
                if (current.Maplist[i].Num == 0) // check if first room
                {
                    current.MapObjList[i] = GameObject.Instantiate(map.StageLoad(ROOMTYPE.Start));
                }
                else if (current.Maplist[i].Num == current.NowCount - 1) // check if last room
                {
                    current.MapObjList[i] = GameObject.Instantiate(map.StageLoad(ROOMTYPE.Boss));
                }
                else
                {
                    if (current.Maplist[i].RightMap != null)
                    {
                        dir[(int)Door.DoorType.Right] = true;
                        countDoor++;
                    }
                    if (current.Maplist[i].LeftMap != null)
                    {
                        dir[(int)Door.DoorType.Left] = true;
                        countDoor++;
                    }
                    if (current.Maplist[i].UpMap != null)
                    {
                        dir[(int)Door.DoorType.Up] = true;
                        countDoor++;
                    }
                    if (current.Maplist[i].DownMap != null)
                    {
                        dir[(int)Door.DoorType.Down] = true;
                        countDoor++;
                    }

                    bool pray = false;
                    ROOMSIZE roomSize = ROOMSIZE.Small;
                    if (countDoor <= 2)//길이 하나 또는 두개인 방은 무조건 작은방 이면서 상점과, 음식점이 없으면 상점과 음식점이 된다.
                    {

                    }
                    else
                    {
                        int rnd = Random.Range(1, 101);
                        if (rnd <= 70)
                        {
                            roomSize = ROOMSIZE.Medium;
                        }
                        else
                        {
                            roomSize = ROOMSIZE.Large;
                        }
                    }

                    roomSize = ROOMSIZE.Small;
                    

                    GameObject obj = (pray == false) ? map.StageLoad(ROOMTYPE.Normal, roomSize) : map.StageLoad(ROOMTYPE.Pray);
                    current.MapObjList[i] = GameObject.Instantiate(obj, mapParent.transform);

                    if (roomSize == ROOMSIZE.Small)
                    {
                        if (!pray) // Pray가 아닌 경우에만 후보 목록에 추가
                        {
                            current.PrayRoomCandidates.Add(i);
                        }
                    }
                }
                
            }

            if (current.MapObjList[i] != null)
            {
                for (int a = 0; a < (int)Door.DoorType.DoorMax; a++)
                {
                    //문이 존재 해야 하는데 생성된 맵이 해당위치에 문이 없는 방이면 다시뽑게 한다.
                    if (dir[a])
                    {
                        if (current.MapObjList[i].GetComponent<BaseStage>().door[a] == null)
                        {
                            flag = true;
                            GameObject.Destroy(current.MapObjList[i]);
                            break;
                        }
                    }
                }
            }
            if (flag)
            {
                i--;
                continue;
            }

            //프리팹 리스트에 값이 들어갔으면 주변에 있는 방들을 검사해서 링크데이터를 넣어준다.
            if (current.MapObjList[i] != null)
            {
                LinkedStage data = SetLinkingData(i);
                current.MapObjList[i].GetComponent<BaseStage>().StageLinkedData = data;
                //obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));
                //Debug.Log($"{i}번방 링크세팅");
            }
        }
    }

    public void MakeStatueRoom()
    {
        if (jajiOption.MaxStatue > current.StatueCount)
        {
            int randomIndex = Random.Range(0, current.PrayRoomCandidates.Count);
            int prayRoomIndex = current.PrayRoomCandidates[randomIndex];
            GameObject prayRoom = map.StageLoad(ROOMTYPE.Pray);
            GameObject.Destroy(current.MapObjList[prayRoomIndex]);
            current.MapObjList[prayRoomIndex] = GameObject.Instantiate(prayRoom);
            current.StatueCount++;

            if (current.MapObjList[prayRoomIndex].GetComponent<BaseStage>().StageLinkedData != null)
            {
                LinkedStage data = SetLinkingData(prayRoomIndex);
                current.MapObjList[prayRoomIndex].GetComponent<BaseStage>().StageLinkedData = data;
            }
        }
    }
}
