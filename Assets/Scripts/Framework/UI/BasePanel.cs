using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> _dic = new Dictionary<string, List<UIBehaviour>>();
    void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Slider>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<ScrollRect>();
    }

    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }

    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (_dic.ContainsKey(controlName))
        {
            for (int i = 0; i < _dic[controlName].Count; i++)
            {
                if (_dic[controlName][i] is T)
                {
                    return _dic[controlName][i] as T;
                }
            }
        }

        return null;
    }

    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();

        string objName;
        for (int i = 0; i < controls.Length; i++)
        {
            objName = controls[i].gameObject.name;
            if (_dic.ContainsKey(objName))
            {
                _dic[objName].Add(controls[i]);
            }
            else
            {
                _dic.Add(objName, new List<UIBehaviour>(){controls[i]});
            }
        }
    }
}
