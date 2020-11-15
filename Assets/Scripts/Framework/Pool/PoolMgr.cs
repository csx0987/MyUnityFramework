

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class PoolData
{
    private readonly GameObject _par;

    public readonly List<GameObject> PoolList;

    public PoolData(GameObject obj, GameObject par)
    {
        _par = new GameObject(obj.name);

        _par.transform.parent = par.transform;

        PoolList = new List<GameObject>() {obj};

        obj.transform.parent = _par.transform;
        obj.SetActive(false);
    }

    public void PushObj(GameObject obj)
    {
        obj.SetActive(false);
        PoolList.Add(obj);
        obj.transform.parent = _par.transform;
    }

    public void GetObj(UnityAction<GameObject> callback = null)
    {
        GameObject obj = PoolList[0];
        PoolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;

        callback?.Invoke(obj);
    }
}

public class PoolMgr : SingletonBase<PoolMgr>
{
    private readonly Dictionary<string, PoolData> _dic = new Dictionary<string, PoolData>();
    private GameObject _poolNode;

    
    public void GetObj(string name, UnityAction<GameObject> callback = null)
    {

        if (_dic.ContainsKey(name) && _dic[name].PoolList.Count > 0)
        {
            _dic[name].GetObj(callback);
        }
        else
        {
            ResMgr.Instance.LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callback?.Invoke(o);
            });
        }
    }

    public void PushObj(string name, GameObject obj)
    {
        if (_poolNode == null)
        {
            _poolNode = new GameObject("pool");
        }

        obj.transform.parent = _poolNode.transform;
        
        if (_dic.ContainsKey(name))
        {
            _dic[name].PushObj(obj);
        }
        else
        {
            _dic.Add(name, new PoolData(obj, _poolNode));
        }
    }

    public void Clean()
    {
        _dic.Clear();
        _poolNode = null;
    }
}
