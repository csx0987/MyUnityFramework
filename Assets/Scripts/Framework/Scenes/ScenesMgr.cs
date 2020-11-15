using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesMgr : SingletonBase<ScenesMgr>
{
    // 同步加载
    public void LoadScene(string name, UnityAction callback)
    {
        SceneManager.LoadScene(name);
        callback();
    }

    // 异步加载
    public void LoadSceneAsyn(string name, UnityAction callback = null)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsync(name, callback));
    }

    private IEnumerator ReallyLoadSceneAsync(string name, UnityAction callback = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            EventCenter.Instance.EventTrigger("loading", ao.progress);
            yield return ao.progress;
        }

        callback?.Invoke();
    }
}
