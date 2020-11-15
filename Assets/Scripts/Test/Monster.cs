using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string name = "123";
    // Start is called before the first frame update
    void Start()
    {
        Dead();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Dead()
    {
        Debug.Log("Monster Dead");
        EventCenter.Instance.EventTrigger("MonsterDead", this);
    }
}
