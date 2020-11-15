using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : SingletonBase<EventCenter>
{
    private Dictionary<string, UnityAction<object>> _eventDic = new Dictionary<string, UnityAction<object>>();

    public void AddEventListener(string name, UnityAction<object> action)
    {
        if (_eventDic.ContainsKey(name))
        {
            _eventDic[name] += action;
        }
        else
        {
            _eventDic.Add(name, action);
        }       
    }

    public void EventTrigger(string name, object param)
    {
        if (_eventDic.ContainsKey(name))
        {
            _eventDic[name](param);
        }
    }

    public void RemoveEventListener(string name, UnityAction<object> action)
    {
        if (_eventDic.ContainsKey(name))
            _eventDic[name] -= action;
    }
}