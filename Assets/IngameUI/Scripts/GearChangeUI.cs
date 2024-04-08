using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearGroupDTO {
    public GearSO data;
    public GearStat stat;
}

public class GearChangeUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] int maxGear = 4; // 최대 장착 가능한 수
    [SerializeField] Transform _main;
    [SerializeField] Transform _content;
    
    //////////////// 설명 관련
    [Header("Description")]
    [SerializeField] Image desc_image;
    [SerializeField] TextMeshProUGUI desc_title;
    [SerializeField] TextMeshProUGUI desc_subText;
    [SerializeField] TextMeshProUGUI desc_script;

    ////////////// 인벤 관련
    [Header("Inventory")]
    [SerializeField] Transform inven_content;
    [SerializeField] GameObject inven_box;

    /////////////// 기어 관련
    [Header("Gear")]
    [SerializeField] GameObject _gearPrefab;
    [SerializeField] GameObject _cogPrefab;
    [SerializeField] Vector2 _gearSize; // 기어 사이즈 강제 변환

    [Header("Scripts")]
    [SerializeField] GearManager _gearManager;
    [SerializeField] ContextMenuUI _contextUI;

    [SerializeField] GearSO[] _gearDatas; // 테스트만 하고 삭제 예정 (나중에 gearManager에서 가져올 예정)

    List<GearGroupDTO> gearDatas;

    GearGroupDTO[] inventory = new GearGroupDTO[9 * 6];

    private void Awake() {
        gearDatas = new();

        for (int i = 0; i < inven_content.childCount; i++)
            DestroyImmediate(inven_content.GetChild(i).gameObject);
        for (int i = 0; i < inventory.Length; i++) {
            Instantiate(inven_box, inven_content).GetComponent<GearChangeHoverEvent>().OnLeftMouseDownEvent += () => OnPointerDown(null);
        }

        // close 버튼연결
        _main.Find("Header/Close").GetComponent<Button>().onClick.AddListener(Close);

        // 설명 init
        HideDescription();

        foreach (var item in _gearDatas) {
            // GearAdd(item);
            print($"{item.Name} inven add {GiveInventory(new GearGroupDTO() { data = item })}");
        }
    
        // Open();
    }

    ////////////////////// TEST
    private void Update() {
        if (Input.GetKeyUp(KeyCode.E)) {
            Open();
        }
    }
    ////////////////////// TEST END

    // 메뉴 오픈
    public void Open() {
        bool needRefresh = false;
        GearGroupDTO[] slot =  _gearManager.GetSlotGearSO();

        if (gearDatas.Count != slot.Length) needRefresh = true;
        if (!needRefresh)
            for (int i = 0; i < slot.Length; i++) {
                if (gearDatas[i].data != slot[i].data || gearDatas[i].stat.Equals(slot[i].stat)) {
                    needRefresh = true;
                    break;
                }
            }

        if (needRefresh) { // 무결성 검사 실패
            // 기어 있는거 다 삭제
            for (int i = 0; i < _content.childCount; i++)
            {
                Destroy(_content.GetChild(i).gameObject);
                gearDatas = new();
            }

            foreach (var item in slot)
                GearAdd(item);
        }

        _main.gameObject.SetActive(true);
    }
    public void Close() {
        _main.gameObject.SetActive(false);
    }

    // void GearAdd(GearSO gearInfo, GearStat stat) {
    void GearAdd(GearGroupDTO gearGroup) {
        GearSO gearInfo = gearGroup.data;
        gearDatas.Add(gearGroup);

        GameObject gearObj = Instantiate(_gearPrefab, _content);
        RectTransform gearTrm = gearObj.GetComponent<RectTransform>();

        Vector2 ratioSize = _gearSize / gearTrm.sizeDelta;
        gearTrm.sizeDelta = _gearSize;
        
        var eventHandler = gearObj.GetComponent<GearChangeHoverEvent>();
        eventHandler.OnHoverEvent += (isHover) => {
            if (isHover) {
                ShowDescription(gearInfo, gearGroup.stat);
            } else {
                HideDescription();
            }
        };
        eventHandler.OnLeftMouseDownEvent += () => {
            _contextUI.Close();
            for (int idx = 0; idx < _content.childCount; idx++)
            {
                if (_content.GetChild(idx) == gearObj.transform) {
                    if (idx == 0) return; // 첫번째는 뺄 수 없음.

                    GearRemove(idx);
                    _gearManager.GearRemove(idx);
                    GiveInventory(gearGroup);
                    break;
                }
            }
        };

        // 콕 소환
        int i = 0;
        foreach (var item in gearInfo.CogList)
        {
            GameObject cogObj = Instantiate(_cogPrefab, gearTrm);
            RectTransform cogTrm = cogObj.GetComponent<RectTransform>();

            cogTrm.sizeDelta = new Vector2(cogTrm.sizeDelta.x * ratioSize.x, cogTrm.sizeDelta.y * ratioSize.y);
            cogTrm.anchoredPosition = new Vector2(cogTrm.anchoredPosition.x * ratioSize.x, cogTrm.anchoredPosition.y * ratioSize.y);

            cogObj.transform.RotateAround(gearTrm.position, new Vector3(0, 0, 1), (360f / gearInfo.CogList.Length) * -i);
            
            // 색상 지정
            // if (item != CogType.None)
                cogObj.GetComponent<Image>().color = GearCircle.GetCogTypeColor(item);

            i++;
        }
    }

    void GearRemove(int idx) {
        Destroy(_content.GetChild(idx).gameObject);
        gearDatas.RemoveAt(idx);
    }

    bool GiveInventory(GearGroupDTO gear, int idx = -1) {
        if (idx == -1) {
            idx = GetEmptyInventoryIdx();
            if (idx == -1) return false;
        }

        if (inventory[idx] != null) return false; // 해당자리에는 이미 있음

        inventory[idx] = gear;
        
        var invenEntity = inven_content.GetChild(idx);
        var imageBox = invenEntity.GetChild(0).GetComponent<Image>();
        imageBox.enabled = true;
        imageBox.sprite = gear.data.Icon;

        var eventManager = invenEntity.GetComponent<GearChangeHoverEvent>();
        eventManager.ClearEventHandler();
        eventManager.OnHoverEvent += (isHover) => {
            if (isHover)
                ShowDescription(gear.data, gear.stat);
            else
                HideDescription();
        };
        eventManager.OnRightMouseDownEvent += () => {
            _contextUI.OpenMenu(new ContextButtonDTO[] {
                new() {
                    name = "장착하기",
                    callback = () => InsertGear(idx)
                },
                new() {
                    name = "버리기",
                    callback = () => {
                        _contextUI.Close();
                        RemoveInventory(idx);
                    }
                }
            });
        };
        eventManager.OnLeftMouseDownEvent += () => OnPointerDown(null);
        
        return true;
    }
    
    bool RemoveInventory(int idx) {
        if (inventory[idx] == null) return false;
        
        inventory[idx] = null;

        var invenEntity = inven_content.GetChild(idx);
        
        var imageBox = invenEntity.GetChild(0).GetComponent<Image>();
        imageBox.enabled = false;
        imageBox.sprite = null;

        var eventHandler = invenEntity.GetComponent<GearChangeHoverEvent>();
        eventHandler.ClearEventHandler();
        eventHandler.OnLeftMouseDownEvent += () => OnPointerDown(null);

        return true;
    }

    int GetEmptyInventoryIdx() {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
                return i;
        }

        return -1;
    }

    // 설명띄움
    void ShowDescription(GearSO gearInfo, GearStat stat) {
        desc_title.text = gearInfo.Name;
        desc_image.sprite = gearInfo.Icon;
        desc_subText.text = $"톱니개수: {gearInfo.CogList.Length}개\nLv.{stat.level}{(gearInfo.ActiveDamage ? $"\n데미지: {stat.damage}" : "")}{(gearInfo.ActiveRange ? $"\n범위: {stat.range}" : "")}";
        desc_script.text = gearInfo.Desc;
    }

    void HideDescription() {
        desc_title.text = "";
        desc_image.sprite = null;
        desc_subText.text = "";
        desc_script.text = "설명을 보려면 기어, 슬롯을 마우스에 올려두세요.";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _contextUI.Close();
    }
    
    // 기어 적용
    void InsertGear(int invenID) {
        _contextUI.Close();
        if (inventory[invenID] == null || gearDatas.Count >= maxGear) return;
        
        var gearD = inventory[invenID];
        RemoveInventory(invenID);
        GearAdd(gearD);
        _gearManager.GearAdd(gearD.data, gearD.stat);
    }
}
