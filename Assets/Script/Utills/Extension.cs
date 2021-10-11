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
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }
    public static bool IsVallid(this GameObject go) // 게임오브잭트 체크
    {
        return go != null && go.activeSelf; // 게임오브젝트가 비어있거나 활성화 되지 않은경우.
    }
}
