using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //매니저는 Singleton Pattern
    static Manager s_instance;
    public static Manager Instance { get { return s_instance; } }


    void Start()
    { 


    }

    void Update()
    {
      
    }
}
