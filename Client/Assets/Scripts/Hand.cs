using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Dictionary<int, Units> _hand = new Dictionary<int, Units>();
    GameObject go;

    public void AddHand(Units unit)
    {
        for (int i=0; i<9; i++)
        {
            if (!_hand.ContainsKey(i))
            {
                _hand.Add(i, unit);
                Renew(i);
                return;
            }
        }
        
    }

    void HandSwap()
    {

    }
    public void Sell(GameObject selected)
    {
        UnitInfoClose();
        _hand.Remove(Convert.ToInt32(selected.transform.parent.name) - 1);
        Destroy(selected);

    }

    void Renew(int i)
    {
        
        Transform parent = transform.GetChild(i);
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{_hand[i]}");
        Instantiate(prefab, parent);
        
    }
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

    void UnitInfo(Vector3 mousepos,GameObject selected)
    {
        go.transform.position = new Vector3(mousepos.x + 30, mousepos.y + 50, mousepos.z);
        go.SetActive(true);
        go.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Sell(selected));

    }

    void UnitInfoClose()
    {
        go.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        go.SetActive(false);
    }
    void Start()
    {
        go = GameObject.Find("Unit_Information");
        go.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }
        
    }

}
