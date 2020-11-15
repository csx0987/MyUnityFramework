using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener("MonsterDead", LevelUp);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("MonsterDead", LevelUp);
    }

    void LevelUp(object param)
    {
        Debug.Log("Level Up" + (param as Monster).name);
    }
}
