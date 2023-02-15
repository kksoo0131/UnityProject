using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Units
{
    Null,
    Knight,
    Wizard,
    Warrior,
    Archer,
    Bandit,
}

public class UnitManager
{
    GameObject Hand;
    public Dictionary<int, Units> _handList = new Dictionary<int, Units>();

    GameObject Shop;
    public List<Units> _shopList = new List<Units>();

    GameObject Info;
    

    public void Init()
    {
        if (Hand == null)
            Hand = GameObject.Find("Hand");
        if (Shop == null)
            Shop = GameObject.Find("Shop");
        if (Info == null)
        {
            Info = GameObject.Find("Unit_Information");
            Info.SetActive(false);
        }
            
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }
    }

    // 상점 이미지 교체
    public void ShopRenew(int index)
    {
        if(Shop == null)
            Init();
        
        Image bn = Shop.transform.GetChild(index + 1).GetChild(0).GetComponent<Image>();
        // 이부분은 나중에
        // Units에 따라 해당 이미지로 이미지를 변경
        switch (_shopList[index])
        {
            case Units.Null:
                bn.color = Color.green;
                break;
            case Units.Knight:
                bn.color = Color.blue;
                break;
            case Units.Wizard:
                bn.color = Color.black;
                break;
            case Units.Warrior:
                bn.color = Color.red;
                break;
            case Units.Archer:
                bn.color = Color.yellow;
                break;
            case Units.Bandit:
                bn.color = Color.cyan;
                break;
        }

    }

    // 핸드에 유닛 오브젝트 생성
    public void AddHand(int index)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!_handList.ContainsKey(i))
            {
                _handList.Add(i, _shopList[index]);
                Transform parent = Hand.transform.GetChild(i);
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/{_handList[i]}");
                UnityEngine.Object.Instantiate(prefab, parent);
                return;
            }
        }
        
        

    }

    // 유닛 정보창 열기
    void UnitInfo(Vector3 mousepos, GameObject selected)
    {
        Info.transform.position = new Vector3(mousepos.x + 30, mousepos.y + 50, mousepos.z);
        Info.SetActive(true);
        Text[] texts = Info.transform.GetComponentsInChildren<Text>();
        texts[0].text = selected.name;
        Info.GetComponentInChildren<Button>().onClick.AddListener(() => Info.GetComponentInParent<UI_Button>().Sell(selected));
    }

    // 유닛 정보창 닫기
    public void UnitInfoClose()
    {
        Info.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        Info.SetActive(false);
    }


    // 나중에 InputManager로 빼야됨
    void SelectUnit()
    {
        // screen상에서 찍은 마우스의 위치 Camera.main.nearClipPlane = 카메라에서 screen까지의 거리
        Vector3 screen_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        // screen상에 있는 마우스의 좌표를 world좌표로 환산.
        Vector3 world_mousePos = Camera.main.ScreenToWorldPoint(screen_mousePos);
        // 카메라에서 마우스로 찍은 월드좌표까지의 거리
        Vector3 dir = world_mousePos - Camera.main.transform.position;
        // 방향만 추출
        dir = dir.normalized;

        RaycastHit hit;

        // 카메라에서 dir방향(마우스로 찍은 좌표의 방향)으로 Raycast
        if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100f))
        {
            // EventSystem.current.IsPointerOverGameObject()
            // 마우스가 UI를 가르킨다면 return
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (hit.collider.gameObject.tag == "Unit")
            {
                UnitInfoClose();
                UnitInfo(screen_mousePos, hit.collider.gameObject);
            }
            else
            {
                UnitInfoClose();
            }


        }

    }

}
