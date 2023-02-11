using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    enum Buttons 
    {
        PointButton,
        A,
        B,
    }

    
    void Start()
    {
        string[] names = Enum.GetNames(typeof(Buttons));
        foreach (string name in names) 
        {
            Debug.Log($"{name}");
        }
        
        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];


    }

    

    void Update()
    {
        
    }
}
