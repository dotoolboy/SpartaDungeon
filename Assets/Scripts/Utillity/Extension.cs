using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{

    // 확장메서드
    public static void BindEvent(this GameObject obj, Action<PointerEventData> action = null, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(obj, action, type);
    }

    public static void DestroyChilds(this GameObject obj)
    {
        Transform[] children = new Transform[obj.transform.childCount];
        for (int i = 0; i < obj.transform.childCount; i++)
            children[i] = obj.transform.GetChild(i);
        foreach (Transform child in children)
            Main.Resource.Destroy(child.gameObject);
    }

}
