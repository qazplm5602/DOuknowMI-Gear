using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace bbqCode
{
    public class GayManater : CockSingleton<GayManater>
    {
        [SerializeField] private CinemachineVirtualCamera vcam;
        [SerializeField] private MapSpawner mapSpawner;

        public BaseStage CurrentRoom;
        public Player plr;

        private BaseStage StartRoom;

        public Image BlackScreen;
        
        public GameObject MobSpawnEffect ;//{ get; private set; }

        protected override void Awake()
        {
            base.Awake();
            //MobSpawnEffect = Resources.Load("Assets/JH_RandomMapGenerate/bbq/SpawnEffect(king)") as GameObject;
           // print(MobSpawnEffect);
        }

        private void Start()
        {
            mapSpawner = FindObjectOfType<MapSpawner>();
            print("its working!");
            Ming();
        }

        //private void Update()
        //{
        //    if (CurrentRoom != null && Input.anyKey)
        //    {
        //        BaseStage canMoveRoom = IsCanMoveRoom(new Vector2Int((int)Input.GetAxisRaw("Horizontal")
        //         , (int)Input.GetAxisRaw("Vertical")));
        //        if ((bool)canMoveRoom)
        //        {
        //            print("ming u moved room");
        //            MoveRoom(canMoveRoom);
        //        }
        //    }
        //}

        [ContextMenu("자위중")]
        public void Ming()
        {
            var startRoom = mapSpawner.current.StartRoom;
            MoveRoom(startRoom,null,false);
            plr.transform.position = startRoom.SpawnPoint.position;
            //plr.gameObject.SetActive(true);
        }

        public void MoveRoom(BaseStage targetRoom, Door door = null, bool transition = true)
        {
            //BlackScreen.color
            if (transition == true)
            {
                BlackScreen.DOColor(new Color(0, 0, 0, 1), .5f).OnComplete(() =>
                {

                    if (CurrentRoom != null)
                        CurrentRoom.Exit();
                    CurrentRoom = targetRoom;
                    CurrentRoom.Enter();
                    Transform doorToGo = door != null ? door.transform : targetRoom.transform;
                    plr.transform.position = doorToGo.position;
                    BlackScreen.DOColor(new Color(0, 0, 0, 0), .5f);
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
