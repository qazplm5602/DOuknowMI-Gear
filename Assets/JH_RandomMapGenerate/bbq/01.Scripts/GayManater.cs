using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace bbqCode
{
    public class GayManater : MonoSingleton<GayManater>
    {
        [SerializeField] private CinemachineVirtualCamera vcam;
        [SerializeField] private MapSpawner mapSpawner;

        private CinemachineConfiner2D confiner2d;

        public BaseStage CurrentRoom;
        public Player plr;

        private BaseStage StartRoom;

        public Image BlackScreen;
        
        public GameObject MobSpawnEffect ;//{ get; private set; }
        public Tween cock;

        private void Awake()
        {
            confiner2d = vcam.GetComponent<CinemachineConfiner2D>();
            //MobSpawnEffect = Resources.Load("Assets/JH_RandomMapGenerate/bbq/SpawnEffect(king)") as GameObject;
            // print(MobSpawnEffect);
        }

        private void Start()
        {
            BlackScreen.color = new Color(0, 0, 0, 1);
            mapSpawner = FindObjectOfType<MapSpawner>();
            plr = FindObjectOfType<Player>();
            cock = BlackScreen.DOColor(new Color(0, 0, 0, 0), .5f);
            Ming();
        }

        [ContextMenu("자위중")]
        public void Ming()
        {
            var startRoom = mapSpawner.current.StartRoom;
            MoveRoom(startRoom,null,false);
            plr.transform.position = startRoom.SpawnPoint.position;
            //plr.gameObject.SetActive(true);
        }
        [ContextMenu("TP TO ELITE ROOM")]
        public void TPELITEROOM()
        {
            MoveRoom(FindAnyObjectByType<EliteMobStage>(),null,true);
        }

        public void MoveRoom(BaseStage targetRoom, Door door = null, bool transition = true)
        {
            //BlackScreen.color
            //transition = false;
            if (transition == true)
            {
                if (cock != null)
                {
                    cock.Kill();
                }
                BlackScreen.DOColor(new Color(0, 0, 0, 1), .5f).OnComplete(() =>
                {
                    if (CurrentRoom != null)
                        CurrentRoom.Exit();
                    CurrentRoom = targetRoom;
                    confiner2d.m_BoundingShape2D = CurrentRoom.transform.Find("Collider").GetComponentInChildren<PolygonCollider2D>();
                    CurrentRoom.Enter();
                    Transform doorToGo = door != null ? door.transform : targetRoom.transform;
                    plr.transform.position = doorToGo.position;
                    BlackScreen.DOColor(new Color(0, 0, 0, 0), .5f);

                    IngameUIControl.Instance.MinimapUI.SetSectionFocus(CurrentRoom.StageNum);
                });
            }
            else
            {
                if (CurrentRoom != null)
                    CurrentRoom.Exit();
                CurrentRoom = targetRoom;
                CurrentRoom.Enter();
                Transform doorToGo = door != null ? door.transform : targetRoom.transform;
                plr.transform.position = doorToGo.position;

                IngameUIControl.Instance.MinimapUI.SetSectionFocus(CurrentRoom.StageNum);
            }
        } 

        public BaseStage IsCanMoveRoom(Vector2Int whereToMove)
        {
            if (CurrentRoom.RoomActive && CurrentRoom.Cleared)
            {
                if (CurrentRoom.StageLinkedData.LeftMap && whereToMove == new Vector2Int(-1, 0)) return CurrentRoom.StageLinkedData.LeftMap;
                if (CurrentRoom.StageLinkedData.RightMap && whereToMove == new Vector2Int(1, 0)) return CurrentRoom.StageLinkedData.RightMap;
                if (CurrentRoom.StageLinkedData.DownMap && whereToMove == new Vector2Int(0, -1)) return CurrentRoom.StageLinkedData.DownMap;
                if (CurrentRoom.StageLinkedData.UpMap && whereToMove == new Vector2Int(0, 1)) return CurrentRoom.StageLinkedData.UpMap;
            }
            return null;
        }
    }

} 
