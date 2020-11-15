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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolMgr.Instance.GetObj("Test/Cube");
        }

        if (Input.GetMouseButtonDown(1))
        {
            PoolMgr.Instance.GetObj("Test/Sphere");
        }
    }
}
