using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {

        return Utill.GetOnAddComponent<T>(go);
    }
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Defind.UIEvent type = Defind.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }
}
