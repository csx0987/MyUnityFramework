using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TestTest
{
    public void Update()
    {
        Debug.Log("Test");
    }
}

public class Test : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<LoginPanel>("LoginPanel", E_UI_Layer.Bot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
