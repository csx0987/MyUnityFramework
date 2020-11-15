using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Push", 3.0f);
    }

    public void Push()
    {
        PoolMgr.Instance.PushObj(gameObject.name, gameObject);
    }
}
