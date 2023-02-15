using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //�Ŵ����� Singleton Pattern
    static Manager s_instance;
    public static Manager Instance { get { Init(); return s_instance; } }

    UnitManager _unit = new UnitManager();

    public static UnitManager Unit { get { return Instance._unit;} }


    void Start()
    {
        Init();
        Unit.Init();
    }

    void Update()
    {
        Unit.Update();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Manager");
            s_instance = go.GetComponent<Manager>();
        }
    }
}
