using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //�ڵ� ����Ʈ�� �ε����� �þ�ٸ�
    public List<Units> _handList = new List<Units>();

    public void AddHand(Units unit)
    {
        _handList.Add(unit);
        Renew(_handList.Count-1);
    }

    void HandSwap()
    {

    }
    void Sell()
    {
        // ���� ������ ������Ʈ�� �θ��� �̸��� Ȯ���մϴ�
        // ����Ʈ���� ���ش� �ڸ��ΰ��� �����ϰ� ������ ������Ʈ�� �����մϴ�.
    }

    void Renew(int i)
    {
        
        Transform parent = transform.GetChild(i);
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{_handList[i]}");
        Instantiate(prefab, parent);
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
