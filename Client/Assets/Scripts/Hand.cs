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
