using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}

public class UIMgr : SingletonBase<UIMgr>
{
    private Dictionary<string, BasePanel> _panelDic = new Dictionary<string, BasePanel>();

    private Transform _bot;
    private Transform _mid;
    private Transform _top;
    private Transform _system;

    private RectTransform _canvas;

    public UIMgr()
    {
        GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
        _canvas = obj.transform as RectTransform;

        _bot = _canvas.Find("Bot");
        _mid = _canvas.Find("Mid");
        _top = _canvas.Find("Top");
        _system = _canvas.Find("System");
        
        GameObject.DontDestroyOnLoad(obj);
        obj = ResMgr.Instance.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Bot, UnityAction<T> callback = null) where T:BasePanel
    {
        if (_panelDic.ContainsKey(panelName))
        {
            _panelDic[panelName].ShowMe();
            if (callback != null)
            {
                callback(_panelDic[panelName] as T);
            }
            return;
        }
        ResMgr.Instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            Transform father = _bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = _mid;
                    break;
                case E_UI_Layer.Top:
                    father = _top;
                    break;
                case E_UI_Layer.System:
                    father = _system;
                    break;
            }
            
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T panel = obj.GetComponent<T>();
            panel.ShowMe();
            if (callback != null)
            {
                callback(panel);
            }
            _panelDic.Add(panelName, panel);
        });
    }

    public void HidePanel(string panelName)
    {
        if (_panelDic.ContainsKey(panelName))
        {
            _panelDic[panelName].HideMe();
            GameObject.Destroy(_panelDic[panelName].gameObject);
            _panelDic.Remove(panelName);
        }
    }

    public T GetPanel<T>(string panelName) where T : BasePanel
    {
        T res = null;
        if (_panelDic.ContainsKey(panelName))
        {
            res = _panelDic.ContainsKey(panelName) as T;
        }
        return res;
    }

    public Transform GetLayerFather(E_UI_Layer layer)
    {
        Transform res = null;
        switch (layer)
        {
            case E_UI_Layer.Bot:
                res = _bot;
                break;
            case E_UI_Layer.Mid:
                res = _mid;
                break;
            case E_UI_Layer.Top:
                res = _top;
                break;
            case E_UI_Layer.System:
                res = _system;
                break;
        }

        return res;
    }
}
