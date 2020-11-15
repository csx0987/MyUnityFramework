using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : SingletonBase<ResMgr>
{
    // 同步加载
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);

        if (res is GameObject)
        {
            res = GameObject.Instantiate(res);
        }

        return res;
    }

    // 异步加载
    public void LoadAsync<T>(string name, UnityAction<T> callback = null) where T : Object
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(name, callback));
    }

    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback = null) where T : Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(name);
        yield return request;

        if (request.asset is GameObject)
        {
            T res = GameObject.Instantiate(request.asset) as T;
            callback?.Invoke(res);
        }
        else
        {
            callback?.Invoke(request.asset as T);
        }
    }
}
