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
        // 마우스가 UI를 가르킨다면 return

       

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

            //첫번째 클릭인경우
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
            //두번째 클릭인경우
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
        // screen상에서 찍은 마우스의 위치 Camera.main.nearClipPlane = 카메라에서 screen까지의 거리
        Vector3 screen_mousePos
            = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        // screen상에 있는 마우스의 좌표를 world좌표로 환산.
        Vector3 world_mousePos = Camera.main.ScreenToWorldPoint(screen_mousePos);
        // 카메라에서 마우스로 찍은 월드좌표까지의 거리
        Vector3 dir = world_mousePos - Camera.main.transform.position;

        Physics.Raycast(Camera.main.transform.position, dir.normalized, out hit, 100f);

        return screen_mousePos;
    }

   


}

