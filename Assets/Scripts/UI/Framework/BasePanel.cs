using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel
{
    public UIType uiType;
    public GameObject activeObj;

    public BasePanel(UIType uitype)
    {
        uiType = uitype;
    }

    public virtual void OnStart()
    {
        Debug.Log($"{uiType.Name}开始使用");
    }

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {

    }

    public virtual void OnDestroy()
    {

    }


}
