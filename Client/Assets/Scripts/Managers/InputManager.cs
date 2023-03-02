using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class InputManager
{
    RaycastHit hit;
    public GameObject forInfo;
    public GameObject forSelect;
    public Vector3 lastmousePos;

    public void Update()
    {
        MouseInput();
    }
    void MouseInput()   
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // ���콺�� UI�� ����Ų�ٸ� return

       

        if (Input.GetMouseButtonDown(1))
        {
            lastmousePos = Raycast();
            if (hit.collider.gameObject.tag == "Unit")
            {
                forInfo = hit.collider.gameObject;
            }
            
           
        }

        else if (Input.GetMouseButtonDown(0))
        {
            lastmousePos = Raycast();

            //ù��° Ŭ���ΰ��
            if (forSelect == null)
            {
                if (hit.collider.gameObject.tag == "Unit")
                {
                    forSelect = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag == "Map" && hit.collider.transform.childCount >0)
                {
                    forSelect = hit.collider.transform.GetChild(0).gameObject;
                }

            }
            //�ι�° Ŭ���ΰ��
            else
            {
                if (hit.collider.gameObject.tag == "Unit")
                {
                    Manager.Unit.UnitSwap(hit.collider.gameObject, forSelect);
                    
                }
                else if (hit.collider.gameObject.tag == "Map")
                {
                    Manager.Unit.UnitSpawn(hit.collider.gameObject, forSelect);
                    
                }

                forSelect = null;
            }
         
        }
             
    }

    Vector3 Raycast()
    {
        // screen�󿡼� ���� ���콺�� ��ġ Camera.main.nearClipPlane = ī�޶󿡼� screen������ �Ÿ�
        Vector3 screen_mousePos
            = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        // screen�� �ִ� ���콺�� ��ǥ�� world��ǥ�� ȯ��.
        Vector3 world_mousePos = Camera.main.ScreenToWorldPoint(screen_mousePos);
        // ī�޶󿡼� ���콺�� ���� ������ǥ������ �Ÿ�
        Vector3 dir = world_mousePos - Camera.main.transform.position;

        Physics.Raycast(Camera.main.transform.position, dir.normalized, out hit, 100f);

        return screen_mousePos;
    }

   


}

