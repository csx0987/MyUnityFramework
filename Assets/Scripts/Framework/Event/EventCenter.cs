using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{
    
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter : SingletonBase<EventCenter>
{
    private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>();

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            _eventDic.Add(name, new EventInfo<T>(action));
        }       
    }
    
    public void AddEventListener(string name, UnityAction action)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            _eventDic.Add(name, new EventInfo(action));
        }       
    }

    public void EventTrigger<T>(string name, T param)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions(param);
        }
    }
    
    public void EventTrigger(string name)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo).actions();
        }
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (_eventDic.ContainsKey(name))
            (_eventDic[name] as EventInfo<T>).actions -= action;
    }
    
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (_eventDic.ContainsKey(name))
            (_eventDic[name] as EventInfo).actions -= action;
    }
}