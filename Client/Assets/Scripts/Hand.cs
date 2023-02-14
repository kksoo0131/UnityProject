using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //핸드 리스트의 인덱스가 늘어낫다면
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
        // 내가 선택한 오브젝트의 부모의 이름을 확인합니다
        // 리스트에서 그해당 자리인것을 제거하고 선택한 오브젝트도 제거합니다.
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
