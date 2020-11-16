using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : SingletonBase<InputMgr>
{
    private bool _isStart = false;
    public InputMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }

    public void StartOrEndCheck(bool isOpen)
    {
        _isStart = isOpen;
    }

    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            EventCenter.Instance.EventTrigger<KeyCode>("keydown", key);
        }

        if (Input.GetKeyUp(key))
        {
            EventCenter.Instance.EventTrigger<KeyCode>("keyup", key);
        }
    }
    
    void Update()
    {
        if (!_isStart) return;
        
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);
    }
}
