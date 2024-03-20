using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSpawner : MonoBehaviour
{
    public enum DIRECTION { NODATA = -1, UP, RIGHT, DOWN, LEFT, MAX };

    public Dictionary<DIRECTION, Vector2Int> dirIndex = new Dictionary<DIRECTION, Vector2Int>()
    {
        { DIRECTION.UP,new Vector2Int(0,1)},
        { DIRECTION.RIGHT,new Vector2Int(1,0)},
        { DIRECTION.DOWN,new Vector2Int(0,-1)},
        { DIRECTION.LEFT,new Vector2Int(-1,0)},
    };

    [Serializable]
    public struct SpawnData
    {
        public int MaxCnt;
        public int MinCnt;

        public int MaxListSize;
    }

    [Serializable]
    public class CurrentValue
    {
        public int NowCount;
        [SerializeField]
        public StageData[] Maplist;
        public List<GameObject> CurrentRoomObjs;
        public GameObject[] MapObjList;
    }

    public SpawnData jajiOption = new SpawnData();
    public CurrentValue current = new CurrentValue();
    public void InitSetting()
    {
        jajiOption.MaxListSize = 5;
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
        ShowMaps();
    }

    public Vector2Int MapSpawn(int x, int y, StageData Parent, int depth)
    {
        //���� �ִ� �������� ����� ���� �����Ѵ�.
        if (current.NowCount >= jajiOption.MaxCnt)
        {
            return new Vector2Int(x, y);
        }

        int RandNum;
        int yval = jajiOption.MaxListSize;

        //������ �ش� ��ġ�� ���� ����� 
        //������, ����, �� ũ��, ���� ���� ���� �����Ѵ�.
        //�������� �־�� �ϸ� �����浵 ����
        if (current.Maplist[x + (y * yval)] == null)
        {
            current.Maplist[x + (y * yval)] = new StageData();
            current.Maplist[x + (y * yval)].InitSttting(current.NowCount, x, y);
            current.NowCount++;

            //�� ����� ���� ����Ǿ �̵��� �� �־�� ������ ������ �ִٰ� �׻� ����Ǿ� �־�� �ϴ°��� �ƴϴ�
            //���� Ž���� �Ҷ� �ڽ��� ���� Ž���� ��ġ���� ������ �־��� ��ġ�� �Ѱ������ν�
            //Ž���� ������ ��ΰ� ���� �̾��ִ� ��ΰ� �� �� �ֵ��� ���ش�.
            if (Parent != null)
            {
                if (Parent.indexX == x)
                {
                    if (Parent.indexY > y)//�Ʒ��ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].DownMap = Parent;
                        Parent.UpMap = current.Maplist[x + (y * yval)];
                    }
                    else//���ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].UpMap = Parent;
                        Parent.DownMap = current.Maplist[x + (y * yval)];


                    }
                }
                else if (Parent.indexY == y)
                {
                    if (Parent.indexX > x)//�����ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].RightMap = Parent;
                        Parent.LeftMap = current.Maplist[x + (y * yval)];
                    }
                    else//���ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].LeftMap = Parent;
                        Parent.RightMap = current.Maplist[x + (y * yval)];
                    }
                }
            }

        }

        //õ��° ���� �׻� ���������ΰ���.
        if (current.NowCount == 1)
        {
            MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
            //return jajiOption.MaxNum;
            return new Vector2Int(x + 1, y);
        }

        //����
        RandNum = UnityEngine.Random.Range(0, 100);
        //Debug.Log($"����{RandNum}");
        if (RandNum <= 70)
        {
            if (x - 1 >= 1 && current.Maplist[(x - 1) + (y * yval)] == null)
            {
                MapSpawn(x - 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //������
        RandNum = UnityEngine.Random.Range(0, 100);

        if (RandNum <= 70)
        {
            if (x + 1 <= jajiOption.MaxListSize - 1 && current.Maplist[(x + 1) + (y * yval)] == null)
            {
                MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //����
        RandNum = UnityEngine.Random.Range(0, 100);
        if (RandNum <= 70)
        {
            if (y - 1 >= 0 && current.Maplist[x + ((y - 1) * yval)] == null)
            {
                MapSpawn(x, y - 1, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //�Ʒ���
        RandNum = UnityEngine.Random.Range(0, 100);

        if (RandNum <= 70)
        {
            if (y + 1 <= jajiOption.MaxListSize - 1 && current.Maplist[x + ((y + 1) * yval)] == null)
            {
                MapSpawn(x, y + 1, current.Maplist[(x) + (y * yval)], depth + 1);
            }
        }

        //�̷��� Ȯ���� �����̵��� �ϸ� �ּҰ����� ����� ���� ������ �ֱ� ������ �ּ� ������ ä������ ������ 4 ������ ����ִ� ���� ã�Ƽ� ������ �������� �ݴϴ�.
        if (current.NowCount < jajiOption.MinCnt)
        {
            for (int i = 0; i < 4; i++)
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
        int interval = 5;
        int yval = jajiOption.MaxListSize;

        for (int y = 0; y < jajiOption.MaxListSize; y++)
        {
            for (int x = 0; x < jajiOption.MaxListSize; x++)
            {
                if (current.MapObjList[x + (y * yval)] != null)
                {
                    GameObject obj = current.MapObjList[x + (y * yval)];
                    obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));

                    int num = obj.GetComponent<BaseStage>().StageLinkedData.Num;
                    obj.name = $"Room_{num}";

                    obj.GetComponent<BaseStage>().Init();
                    //obj.GetComponent<BaseStage>().StageNum = num;
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

        //linkeddata���� ��������� ����Ǿ� �ִ� �� Ȯ���ϰ�
        //prefabs���� �ش���⿡ ���� ���� ������� �ִ��� Ȯ���ϰ�
        //���� �����Ѵ�.

        if (current.Maplist[index].LeftMap != null)
        {
            if (current.MapObjList[(x - 1) + (y * yval)] != null)
            {
                linkeddata.LeftMap = current.MapObjList[(x - 1) + (y * yval)];
                if (current.MapObjList[(x - 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[(x - 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData.RightMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if (current.Maplist[index].RightMap != null)
        {
            if (current.MapObjList[(x + 1) + (y * yval)] != null)
            {
                linkeddata.RightMap = current.MapObjList[(x + 1) + (y * yval)];

                if (current.MapObjList[(x + 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[(x + 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData.LeftMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if (current.Maplist[index].UpMap != null)
        {
            if (current.MapObjList[x + ((y - 1) * yval)] != null)
            {
                linkeddata.UpMap = current.MapObjList[x + ((y - 1) * yval)];

                if (current.MapObjList[x + ((y - 1) * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[x + ((y - 1) * yval)].GetComponent<BaseStage>().StageLinkedData.DownMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if (current.Maplist[index].DownMap != null)
        {
            if (current.MapObjList[x + ((y + 1) * yval)] != null)
            {
                linkeddata.DownMap = current.MapObjList[x + ((y + 1) * yval)];

                if (current.MapObjList[x + ((y + 1) * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[x + ((y + 1) * yval)].GetComponent<BaseStage>().StageLinkedData.UpMap = current.MapObjList[(x) + (y * yval)];
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
            int count = 0;
            bool flag = false;

            for (int b = 0; b < dir.Length; b++)
            {
                dir[b] = false;
            }


            if (current.Maplist[i] != null)
            {
                if (current.Maplist[i].RightMap != null)
                {
                    dir[(int)Door.DoorType.Right] = true;
                    count++;
                }
                if (current.Maplist[i].LeftMap != null)
                {
                    dir[(int)Door.DoorType.Left] = true;
                    count++;
                }
                if (current.Maplist[i].UpMap != null)
                {
                    dir[(int)Door.DoorType.Up] = true;
                    count++;
                }
                if (current.Maplist[i].DownMap != null)
                {
                    dir[(int)Door.DoorType.Down] = true;
                    count++;
                }
                int rnd = Random.Range(1, 101);
                if (rnd < 1e6)
                {
                    GameObject obj = Map.Instance.StageLoad(Map.ROOMTYPE.Normal);
                    current.MapObjList[i] = GameObject.Instantiate(obj);
                }
                
            }

            if (current.MapObjList[i] != null)
            {
                for (int a = 0; a < (int)Door.DoorType.DoorMax; a++)
                {
                    //���� ���� �ؾ� �ϴµ� ������ ���� �ش���ġ�� ���� ���� ���̸� �ٽṵ̂� �Ѵ�.
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

            //������ ����Ʈ�� ���� ������ �ֺ��� �ִ� ����� �˻��ؼ� ��ũ�����͸� �־��ش�.
            if (current.MapObjList[i] != null)
            {
                LinkedStage data = SetLinkingData(i);
                current.MapObjList[i].GetComponent<BaseStage>().StageLinkedData = data;
                //obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));
                Debug.Log($"{i}���� ��ũ����");
            }
        }
    }
}
