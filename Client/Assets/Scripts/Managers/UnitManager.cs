using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UnitManager
{
    GameObject Hand;
    public void Init()
    {
        Hand = GameObject.Find("Hand");
    }

    public bool Add(Units unit)
    {
        for (int i = 0; i < 9; i++)
        {
            if (Hand.transform.GetChild(i).childCount == 0)
            {
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/Units/{unit}");
                UnityEngine.Object.Instantiate(prefab, Hand.transform.GetChild(i));
                return true;
            }
        }
        return false;

    }
    public void UnitSpawn(GameObject key, GameObject go)
    {
        if (key.transform.childCount > 0)
            UnitSwap(key.transform.GetChild(0).gameObject, go);

        go.transform.parent = key.transform;
        go.transform.position = key.transform.position;

    }

    public void UnitSwap(GameObject A, GameObject B)
    {
        Transform temp = A.transform.parent;

        A.transform.parent = B.transform.parent;
        B.transform.parent = temp;

        A.transform.position = A.transform.parent.position;
        B.transform.position = B.transform.parent.position;
    }

   
}
