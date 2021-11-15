using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utill
{

   
    public static T GetOnAddComponent<T>(GameObject go) where T : Component
    {
        T Component = go.GetComponent<T>();
        if (Component == null)
            Component = go.AddComponent<T>();
        return Component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) // ���ӿ�����Ʈ �������� �ڽ��� ã������ �Լ�
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }
    //recursive = ���ѵ� ���ڰ������ ���� �Ұ�����. 
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object // �Ϲ�ȭ.
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }


}
