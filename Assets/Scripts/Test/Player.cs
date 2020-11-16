using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener<Monster>("MonsterDead", LevelUp);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<Monster>("MonsterDead", LevelUp);
    }

    void LevelUp(Monster param)
    {
        Debug.Log("Level Up" + param.name);
    }
}
