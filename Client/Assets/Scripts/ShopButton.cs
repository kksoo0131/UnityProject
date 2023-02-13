using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

public class ShopButton : MonoBehaviour
{
    List<Units> _handList = new List<Units>();
    List<Units> _shopList = new List<Units>();
   
    public void Roll()
    {
        // 리스트를 Roll
        _shopList.Clear();
        for (int i = 0; i < 5; i++)
        {
            // 유니티에서 제공하는 랜덤함수 Random.Range(min,max)
            int rand = UnityEngine.Random.Range(1, 6);
            _shopList.Add((Units)rand);

            Renew(i);
        }
    }

    public void Renew(int i)
    {
        //이미지 교체
        GameObject go = transform.GetChild(i + 1).GetChild(0).gameObject;
        Image bn = go.GetComponent<Image>();
        // 이부분은 나중에
        // Units에 따라 해당 이미지로 이미지를 변경
        switch (_shopList[i])
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

    public void Buy(int i)
    {
        if (_shopList[i] == 0)
        {
            Debug.Log($"이미 팔린 상품");
            return;
        }
            
        _handList.Add(_shopList[i]);
        _shopList[i] = Units.Null;
        Renew(i);

        // 구매 버튼을 눌렀을때 해당 항목을
        // 내 손패 리스트에 add하고
        // 해당 항목은 null로 바꿈
        // 이미지를 지운다.
        // 항목이 이미 null일때는 눌러도 작동하지 않아야된다.

    }
    void Start()
    {
        Roll();
    }

    void Update()
    { 
        
    }

}
