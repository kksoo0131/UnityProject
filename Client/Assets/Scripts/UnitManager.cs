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

public class UnitManager : MonoBehaviour
{
    GameObject Hand;
    Dictionary<int, Units> _hand = new Dictionary<int, Units>();

    GameObject Shop;
    List<Units> _shopList = new List<Units>();

    GameObject Info;
    

    public void Start()
    {
        Hand = GameObject.Find("Hand");
        Shop = GameObject.Find("Shop");
        Info = GameObject.Find("Unit_Information");
        Info.SetActive(false);
        Roll();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }
    }

    // ��������� �����Ѵ�.
    public void Roll()
    {
        // ����Ʈ�� Roll
        _shopList.Clear();
        for (int i = 0; i < 5; i++)
        {
            // ����Ƽ���� �����ϴ� �����Լ� Random.Range(min,max)
            int rand = UnityEngine.Random.Range(1, 6);
            _shopList.Add((Units)rand);

            ShopRenew(i);
        }

        UnitInfoClose();


    }

    // �������� ������ ����
    public void Buy(int index)
    {
        if (_hand.Count >= 9 || _shopList[index] == 0)
            return;

        AddHand(index);
        _shopList[index] = Units.Null;
        ShopRenew(index);

        UnitInfoClose();
    }

    // ���� �Ǹ�
    public void Sell(GameObject selected)
    {
        _hand.Remove(Convert.ToInt32(selected.transform.parent.name) - 1);
        UnityEngine.Object.Destroy(selected);

        UnitInfoClose();

    }

    // ���� �̹��� ��ü
    void ShopRenew(int index)
    {  
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
    void AddHand(int index)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!_hand.ContainsKey(i))
            {
                _hand.Add(i, _shopList[index]);
                Transform parent = Hand.transform.GetChild(i);
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/{_hand[i]}");
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
        Info.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Sell(selected));

    }

    // ���� ����â �ݱ�
    void UnitInfoClose()
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
