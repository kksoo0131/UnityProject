using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI : MonoBehaviour
{
    GameObject uishop;
    GameObject uiinfo;

    List<Units> _shopList = new List<Units>();

    void Start()
    {
        uishop = gameObject.transform.GetChild(0).gameObject;
        uiinfo = gameObject.transform.GetChild(1).gameObject;
        uiinfo.SetActive(false);
        Roll();
    }

    void Update()
    {
        UnitInfo(Manager.Input.forInfo);
    }
    
    public void Roll()
    {
        // 상점을 갱신
        _shopList.Clear();
        for (int i = 0; i < 5; i++)
        {
            // 유니티에서 제공하는 랜덤함수 Random.Range(min,max)
            int rand = Random.Range(1, 6);
            _shopList.Add((Units) rand);

            Renew(i);
        }

    }
    public void Buy(int idx)
    {
        if (Manager.Unit.Add(_shopList[idx] ) == false)
            return;
        _shopList[idx] = Units.Null;
        Renew(idx);
    }

    public void Renew(int idx)
    {
        // 상점 이미지 교체

        Image bn = uishop.transform.GetChild(idx + 1).GetChild(0).GetComponent<Image>();

        switch (_shopList[idx])
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

    void UnitInfo(GameObject selected)
    {

        if (Manager.Input.forInfo == null)
        {
            uiinfo.SetActive(false);
            return;
        }
            
        uiinfo.SetActive(true);
        // 게임 오브젝트 좌표를 UI좌표로 환산해서 계산해줘야된다.
        uiinfo.transform.position = new Vector3(
                        Manager.Input.lastmousePos.x + 30,
                        Manager.Input.lastmousePos.y + 50,
                        Manager.Input.lastmousePos.z);
        Text[] texts = uiinfo.transform.GetComponentsInChildren<Text>();
        texts[0].text = selected.name;
        uiinfo.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        uiinfo.GetComponentInChildren<Button>().onClick.AddListener(() => Sell(selected));

    }

    public void Sell(GameObject selected)
    {
        UnityEngine.Object.Destroy(selected);
        uiinfo.SetActive(false);
    }
}
