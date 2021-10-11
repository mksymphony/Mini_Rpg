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
    public static bool IsVallid(this GameObject go) // ���ӿ�����Ʈ üũ
    {
        return go != null && go.activeSelf; // ���ӿ�����Ʈ�� ����ְų� Ȱ��ȭ ���� �������.
    }
}
