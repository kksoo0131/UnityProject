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
        // ����Ʈ�� Roll
        _shopList.Clear();
        for (int i = 0; i < 5; i++)
        {
            // ����Ƽ���� �����ϴ� �����Լ� Random.Range(min,max)
            int rand = UnityEngine.Random.Range(1, 6);
            _shopList.Add((Units)rand);

            Renew(i);
        }
    }

    public void Renew(int i)
    {
        //�̹��� ��ü
        GameObject go = transform.GetChild(i + 1).GetChild(0).gameObject;
        Image bn = go.GetComponent<Image>();
        // �̺κ��� ���߿�
        // Units�� ���� �ش� �̹����� �̹����� ����
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
            Debug.Log($"�̹� �ȸ� ��ǰ");
            return;
        }
            
        _handList.Add(_shopList[i]);
        _shopList[i] = Units.Null;
        Renew(i);

        // ���� ��ư�� �������� �ش� �׸���
        // �� ���� ����Ʈ�� add�ϰ�
        // �ش� �׸��� null�� �ٲ�
        // �̹����� �����.
        // �׸��� �̹� null�϶��� ������ �۵����� �ʾƾߵȴ�.

    }
    void Start()
    {
        Roll();
    }

    void Update()
    { 
        
    }

}
