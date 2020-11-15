using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

public class MonoMgr : SingletonBase<MonoMgr>
{
    public MonoController controller;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public Coroutine StartCoroutine_Auto(IEnumerator routine)
    {
        return controller.StartCoroutine_Auto(routine);
    }

    public void StopCoroutine(IEnumerator routine)
    { 
        controller.StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine)
    {
        controller.StopCoroutine(routine);
    }
}