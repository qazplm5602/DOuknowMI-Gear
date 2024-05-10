using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mapParent;
    [SerializeField] private MinimapUI minimapUI;
    private Stack<int> mapIndexStackByNum = new();
    public int tries = 0;
    public enum DIRECTION { NODATA = -1, UP, RIGHT, DOWN, LEFT, MAX };

    public Dictionary<DIRECTION, Vector2Int> dirIndex = new Dictionary<DIRECTION, Vector2Int>()
    {
        { DIRECTION.UP,new Vector2Int(0,1)},
        { DIRECTION.RIGHT,new Vector2Int(1,0)},
        { DIRECTION.DOWN,new Vector2Int(0,-1)},
        { DIRECTION.LEFT,new Vector2Int(-1,0)},
    };

    public Map map;

    #region 기본값ㅄ
    private int _statueChance = 8;
    private int _mediumChance = 50;
    private int _largeChance = 35;
    #endregion

    [Serializable]
    public struct MapSettings
    {
        [HideInInspector]
        public int MaxListSize;

        [Header("Rooms Count Config")]
        public int MaxCnt;
        public int MinCnt;
        public int MaxStatue;
        public int MaxEliteRooms;

        [Header("Chances")]
        public int StatueChance;
        public int MediumRoomChance;
        public int LargeRoomChance;
    }

    [Serializable]
    public class CurrentMapState
    {
        public int NowCount;
        [SerializeField]
        public StageData[] Maplist;
        public List<GameObject> CurrentRoomObjs;
        public List<int> PrayRoomCandidates; // 조각상 방으로 가능한 방들의 인덱스 목록
        public List<int> EliteRoomCandidates; // 엘리트 방으로 가능한 방들의 인덱스 목록
        public BaseStage[] MapObjList;
        public StartStage StartRoom;

        public int LargeRoomChance;
        public int MediumRoomChance;

        public int StatueCount;
        public int EliteRoomCount;

        //작은방이 나올수 있는지를 검사하기 위한 작은 방들의 문 위치 데이터
        public List<Vector4> SmallRoomDoorDatas;
    }

    public MapSettings jajiOption = new MapSettings();
    public CurrentMapState current = new CurrentMapState();
    public void InitSetting()
    {
        map.Init();
        current.PrayRoomCandidates = new List<int>();
        jajiOption.MaxListSize = jajiOption.MaxCnt + 2;
        current.Maplist = new StageData[jajiOption.MaxListSize * jajiOption.MaxListSize];
        current.MapObjList = new BaseStage[jajiOption.MaxListSize * jajiOption.MaxListSize];
        current.CurrentRoomObjs = new List<GameObject>();

        if (jajiOption.LargeRoomChance == 0)
            jajiOption.LargeRoomChance = _largeChance;
        if (jajiOption.MediumRoomChance == 0)
            jajiOption.MediumRoomChance = _mediumChance;
        if (jajiOption.StatueChance == 0)
            jajiOption.StatueChance = _statueChance;

        for (int i = 0; i < jajiOption.MaxListSize * jajiOption.MaxListSize - 1; i++)
        {
            current.Maplist[i] = null;
            current.MapObjList[i] = null;
        }

        foreach(NormalStage stage in map.SmallRoom)
        {
            current.SmallRoomDoorDatas.Add(new Vector4(
                stage.door[0] != null ? 1 : 0,
                stage.door[1] != null ? 1 : 0,
                stage.door[2] != null ? 1 : 0,
                stage.door[3] != null ? 1 : 0));
        }
    }

    public BaseStage BOSS_ROOM;


    private void Start()
    {
        InitSetting();
      
        MapSpawn(0, 5, null, 0);
        SetBossRoom();
        MakeMapMotherfuker();
        MakeStatueRoom();
        MakeEliteRoom();
        ShowMaps();
        minimapUI.MakeMinimap(this);
    }

    private void SetBossRoom()
    {
        var yval = jajiOption.MaxListSize;
        for (int i = 0; i < mapIndexStackByNum.Count; i++)
        {
            int index = mapIndexStackByNum.Pop();

            //Debug.Log(index);

            int _x = index % yval;
            int _y = index / yval;

            if (current.Maplist[_x + ((_y) * yval)] == null)
            {
                continue;
            }

            int tx = _x + 1;
            int ty = _y;
            if (tx >= 0 && tx < jajiOption.MaxListSize - 1 && ty >= 0 && ty < jajiOption.MaxListSize - 1)
            {
                if (current.Maplist[tx + ((ty ) * yval)] == null)
                {
                    print($"{tx}, {ty}");   
                    if (MapSpawn(tx, ty, current.Maplist[(_x) + (_y * yval)], 598, true) != new Vector2Int(-1, -1))
                        return;
                }
            }
        }
    }

    public Vector2Int MapSpawn(int x, int y, StageData Parent, int depth, bool force = false)
    {
        //맵이 최대 개수까지 만들어 지면 종료한다.
        tries++;
        if (current.NowCount >= jajiOption.MaxCnt)
        {
            return new Vector2Int(x, y);
        }

        int RandNum;
        int yval = jajiOption.MaxListSize;

        //tageData thisRoom = current.Maplist[(x) + (y * yval)];

        if (current.Maplist[x + (y * yval)] == null)
        {
            current.Maplist[x + (y * yval)] = new StageData();
            current.Maplist[x + (y * yval)].InitSetting(current.NowCount, x, y);
            current.NowCount++;
            //print($"{current.Maplist[x + (y * yval)].Num} from {Parent.Num}");

            mapIndexStackByNum.Push(x + (y * yval));

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
            //print($"{current.Maplist[x + (y * yval)].Num} : {current.Maplist[x + (y * yval)].UpMap}");
            //print($"{current.Maplist[x + (y * yval)].Num} : {current.Maplist[x + (y * yval)].RightMap}");
            //print($"{current.Maplist[x + (y * yval)].Num} : {current.Maplist[x + (y * yval)].LeftMap}");
            //print($"{current.Maplist[x + (y * yval)].Num} : {current.Maplist[x + (y * yval)].DownMap}");
        }
        else
        {
            return new Vector2Int(-1,-1);
        }

        //천번째 방은 항상 오른쪽으로간다.
        if (force == false)
        {
            if (current.NowCount == 1)
            {
                MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
                //return jajiOption.MaxNum;
                return new Vector2Int(x + 1, y);
            }

            bool 고무현 = false;
            do
            {

                //왼쪽
                RandNum = Random.Range(0, 100);
                //Debug.Log($"랜덤{RandNum}");
                if (RandNum <= 80)
                {
                    if (x - 1 >= 1 && current.Maplist[(x - 1) + (y * yval)] == null)
                    {
                        MapSpawn(x - 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
                        고무현 = true;
                    }
                }

                //오른쪽
                RandNum = Random.Range(0, 100);

                if (RandNum <= 80)
                {
                    if (x + 1 <= jajiOption.MaxListSize - 1 && current.Maplist[(x + 1) + (y * yval)] == null)
                    {
                        MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
                        고무현 = true;
                    }
                }

                //위쪽
                RandNum = Random.Range(0, 100);
                if (RandNum <= 80)
                {
                    if (y - 1 >= 0 && current.Maplist[x + ((y - 1) * yval)] == null)
                    {
                        MapSpawn(x, y - 1, current.Maplist[(x) + (y * yval)], depth + 1);
                        고무현 = true;
                    }
                }

                //아래쪽
                RandNum = Random.Range(0, 100);

                if (RandNum <= 80)
                {

                    if (y + 1 <= jajiOption.MaxListSize - 1 && current.Maplist[x + ((y + 1) * yval)] == null)
                    {
                        MapSpawn(x, y + 1, current.Maplist[(x) + (y * yval)], depth + 1);
                        고무현 = true;
                    }
                }
            } while (!고무현 || current.NowCount < jajiOption.MinCnt);
        }

        //이렇게 확률로 움직이도록 하면 최소개수가 만들어 지지 않을수 있기 때문에 최소 개수가 채워지지 않으면 4 방향중 비어있는 곳을 찾아서 강제로 생성시켜 줍니다.
        //if (current.NowCount < jajiOption.MinCnt)
        //{ 
        //    for (int i = 3; i >= 0; i--)
        //    {
        //        if (!dirIndex.ContainsKey((DIRECTION)i)) continue;
        //        int tempx = x + dirIndex[(DIRECTION)i].x;
        //        int tempy = y + dirIndex[(DIRECTION)i].y;
        //        if (tempx >= 0 && tempx < jajiOption.MaxListSize - 1 && tempy >= 0 && tempy < jajiOption.MaxListSize - 1)
        //        {
        //            if (current.Maplist[tempx + ((tempy) * yval)] == null)
        //            {
        //                MapSpawn(tempx, tempy, current.Maplist[(x) + (y * yval)], depth + 1, true);
        //            }
        //        }
        //    }
        //}

        return new Vector2Int(x, y);
    }

    public void ShowMaps()
    {
        int interval = 90;
        int yval = jajiOption.MaxListSize;

        for (int y = 0; y < jajiOption.MaxListSize; y++)
        {
            for (int x = 0; x < jajiOption.MaxListSize; x++)
            {
                if (current.MapObjList[x + (y * yval)] != null)
                {
                    BaseStage stageData = current.MapObjList[x + (y * yval)];
                    stageData.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));

                    int num = stageData.StageLinkedData.Num;
                    stageData.gameObject.name = $"Room_{num} {stageData.type} {(stageData.type == ROOMTYPE.Normal ? stageData.Size.ToString() : ' ')}";

                    stageData.Init();
                    stageData.StageNum = num;

                    if (stageData.type == ROOMTYPE.Start)
                    {
                        //Debug.Log(stageData.type + stageData.transform.name);
                        current.StartRoom = stageData as StartStage;
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
                if (mapObj.StageLinkedData != null)
                {
                    mapObj.StageLinkedData.RightMap = current.MapObjList[index];
                }
            }
        }
        if (current.Maplist[index].RightMap != null)
        {
            var mapObj = current.MapObjList[(x + 1) + (y * yval)];
            if (mapObj != null)
            {
                linkeddata.RightMap = mapObj;

                if (mapObj.StageLinkedData != null)
                {
                    mapObj.StageLinkedData.LeftMap = current.MapObjList[index];
                }
            }
        }
        if (current.Maplist[index].UpMap != null)
        {
            var mapObj = current.MapObjList[x + ((y - 1) * yval)];
            if (mapObj != null)
            {
                linkeddata.UpMap = mapObj;

                if (mapObj.StageLinkedData != null)
                {
                    mapObj.StageLinkedData.DownMap = current.MapObjList[index];
                }
            }
        }
        if (current.Maplist[index].DownMap != null)
        {
            var mapObj = current.MapObjList[x + ((y + 1) * yval)];
            if (mapObj != null)
            {
                linkeddata.DownMap = mapObj;

                if (mapObj.StageLinkedData != null)
                {
                    mapObj.StageLinkedData.UpMap = current.MapObjList[index];
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


            if (current.Maplist[i] == null) continue;
            if (current.Maplist[i].Num == 0) // check if first room
            {
                current.MapObjList[i] = GameObject.Instantiate(map.StageLoad(ROOMTYPE.Start));
            }
            else if (current.Maplist[i].Num == current.NowCount - 1) // check if last room
            {
                current.MapObjList[i] = GameObject.Instantiate(map.StageLoad(ROOMTYPE.Boss)); // BOSS_ROOM; //GameObject.Instantiate(map.StageLoad(ROOMTYPE.Boss));
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

                ROOMSIZE roomSize = ROOMSIZE.Small;

                if (countDoor <= 2 )//길이 하나 또는 두개인 방은 무조건 작은방이나 기도방이 ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ 된다.
                {
                    //print($"{countDoor} {i}");
                    //진짜입니까??
                    if (20 >= Random.Range(1, 101)) //(50 >= Random.Range(1, 101))
                    {
                        roomSize = ROOMSIZE.Medium;
                    }
                }
                else
                {
                    Debug.LogWarning("시발 떴따~!!!");
                    if (60 >= Random.Range(1,101)) //(50 >= Random.Range(1, 101))
                    {
                        roomSize = ROOMSIZE.Medium;
                    }
                    else// if (35 >= Random.Range(1, 101))
                    {
                        roomSize = ROOMSIZE.Large;
                    }
                }

                //print(countDoor);

                current.EliteRoomCandidates.Add(i);
                BaseStage obj = map.StageLoad(ROOMTYPE.Normal, roomSize);
                current.MapObjList[i] = Instantiate(obj, mapParent.transform);
                obj.Size = roomSize;

                if (roomSize == ROOMSIZE.Small)
                {
                    current.PrayRoomCandidates.Add(i);
                }
            }         

            if (current.MapObjList[i] != null)
            {
                for (int a = 0; a < (int)Door.DoorType.DoorMax; a++)
                {
                    //문이 존재 해야 하는데 생성된 맵이 해당위치에 문이 없는 방이면 다시뽑게 한다.
                    if (dir[a])
                    {
                        if (current.MapObjList[i].door[a] == null)
                        {
                            flag = true;
                            current.PrayRoomCandidates.Remove(i);
                            Destroy(current.MapObjList[i].gameObject);
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

            //link close rooms
            if (current.MapObjList[i] != null)
            {
                LinkedStage data = SetLinkingData(i);
                current.MapObjList[i].StageLinkedData = data;
            }
        }
    }

    public void MakeStatueRoom()
    {
        if (jajiOption.MaxStatue > current.StatueCount)
        {
            int randomIndex = Random.Range(0, current.PrayRoomCandidates.Count);
            int prayRoomIndex = current.PrayRoomCandidates[randomIndex];
            current.PrayRoomCandidates.Remove(randomIndex);
            current.EliteRoomCandidates.Remove(randomIndex);

            Destroy(current.MapObjList[prayRoomIndex].gameObject);
            BaseStage prayRoom = map.StageLoad(ROOMTYPE.Statue);
            current.MapObjList[prayRoomIndex] = GameObject.Instantiate(prayRoom);
            current.StatueCount++;

            LinkedStage data = SetLinkingData(prayRoomIndex);
            current.MapObjList[prayRoomIndex].StageLinkedData = data;
            if (50 >= Random.Range(1, 101))
            {
                MakeStatueRoom();
            }
        }
    }

    public void MakeEliteRoom()
    {
        if (jajiOption.MaxEliteRooms > current.EliteRoomCount)
        {
            int randomIndex = Random.Range(0, current.EliteRoomCandidates.Count);
            int eliteRoomIndex = current.EliteRoomCandidates[randomIndex];
            current.EliteRoomCandidates.Remove(randomIndex);
            current.EliteRoomCount++;

            Destroy(current.MapObjList[eliteRoomIndex].gameObject);
            BaseStage eliteRoom = map.StageLoad(ROOMTYPE.Elite);
            current.MapObjList[eliteRoomIndex] = GameObject.Instantiate(eliteRoom);

            LinkedStage data = SetLinkingData(eliteRoomIndex);
            current.MapObjList[eliteRoomIndex].StageLinkedData = data;
            if (14 >= Random.Range(1, 101))
            {
                MakeEliteRoom();
            }
        }
    }
}
