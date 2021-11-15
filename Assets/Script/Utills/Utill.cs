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
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) // 게임오브젝트 만들어놓은 자식을 찾기위한 함수
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }
    //recursive = 포한된 인자값들또한 색적 할것인지. 
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object // 일반화.
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
