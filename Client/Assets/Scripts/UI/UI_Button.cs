using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    void Start()
    {
        Roll();
    }
    public void Buy(int index)
    {
        
        if (Manager.Unit._handList.Count >= 9 || Manager.Unit._shopList[index] == 0)
            return;

        Manager.Unit.AddHand(index);
        Manager.Unit._shopList[index] = Units.Null;
        Manager.Unit.ShopRenew(index);

        Manager.Unit.UnitInfoClose();
    }

    public void Sell(GameObject selected)
    {
        Manager.Unit._handList.Remove(Convert.ToInt32(selected.transform.parent.name) - 1);
        UnityEngine.Object.Destroy(selected);
        Manager.Unit.UnitInfoClose();

    }

    public void Roll()
    {
        // ����Ʈ�� Roll
        Manager.Unit._shopList.Clear();
        for (int i = 0; i < 5; i++)
        {
            // ����Ƽ���� �����ϴ� �����Լ� Random.Range(min,max)
            int rand = UnityEngine.Random.Range(1, 6);
            Manager.Unit._shopList.Add((Units)rand);

            Manager.Unit.ShopRenew(i);
        }

        Manager.Unit.UnitInfoClose();


    }
}
