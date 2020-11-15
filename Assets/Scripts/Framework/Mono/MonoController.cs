using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction _updateEvent;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_updateEvent != null)
            _updateEvent();
    }

    public void AddUpdateListener(UnityAction fun)
    {
        _updateEvent += fun;
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        _updateEvent -= fun;
    }
}
