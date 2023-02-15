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

    // ���� �̹��� ��ü
    public void ShopRenew(int index)
    {
        if(Shop == null)
            Init();
        
        Image bn = Shop.transform.GetChild(index + 1).GetChild(0).GetComponent<Image>();
        // �̺κ��� ���߿�
        // Units�� ���� �ش� �̹����� �̹����� ����
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

    // �ڵ忡 ���� ������Ʈ ����
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

    // ���� ����â ����
    void UnitInfo(Vector3 mousepos, GameObject selected)
    {
        Info.transform.position = new Vector3(mousepos.x + 30, mousepos.y + 50, mousepos.z);
        Info.SetActive(true);
        Text[] texts = Info.transform.GetComponentsInChildren<Text>();
        texts[0].text = selected.name;
        Info.GetComponentInChildren<Button>().onClick.AddListener(() => Info.GetComponentInParent<UI_Button>().Sell(selected));
    }

    // ���� ����â �ݱ�
    public void UnitInfoClose()
    {
        Info.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        Info.SetActive(false);
    }


    // ���߿� InputManager�� ���ߵ�
    void SelectUnit()
    {
        // screen�󿡼� ���� ���콺�� ��ġ Camera.main.nearClipPlane = ī�޶󿡼� screen������ �Ÿ�
        Vector3 screen_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        // screen�� �ִ� ���콺�� ��ǥ�� world��ǥ�� ȯ��.
        Vector3 world_mousePos = Camera.main.ScreenToWorldPoint(screen_mousePos);
        // ī�޶󿡼� ���콺�� ���� ������ǥ������ �Ÿ�
        Vector3 dir = world_mousePos - Camera.main.transform.position;
        // ���⸸ ����
        dir = dir.normalized;

        RaycastHit hit;

        // ī�޶󿡼� dir����(���콺�� ���� ��ǥ�� ����)���� Raycast
        if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100f))
        {
            // EventSystem.current.IsPointerOverGameObject()
            // ���콺�� UI�� ����Ų�ٸ� return
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
