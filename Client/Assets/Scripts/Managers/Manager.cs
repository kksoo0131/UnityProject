using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //매니저는 Singleton Pattern
    static Manager s_instance;
    public static Manager Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    UnitManager _unit = new UnitManager();
    
    public static InputManager Input { get { return Instance._input; } }
    public static UnitManager Unit { get { return Instance._unit; } }

    void Start()
    {
        Init();
        Unit.Init();
    }

    void Update()
    {
        Input.Update();
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
