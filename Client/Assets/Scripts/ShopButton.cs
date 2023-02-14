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
    
    List<Units> _shopList = new List<Units>();
    public Hand hand;

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

    public void Renew(int index)
    {
        //이미지 교체
        GameObject go = transform.GetChild(index + 1).GetChild(0).gameObject;
        Image bn = go.GetComponent<Image>();
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

    public void Buy(int index)
    {
        if (hand._hand.Count >=9 || _shopList[index] == 0)
        {
            return;
        }            
        hand.AddHand(_shopList[index]);
        _shopList[index] = Units.Null;
        Renew(index);
    }
    void Start()
    {
        hand = GameObject.Find("Hand").GetComponent<Hand>();
        Roll();
    }
}
