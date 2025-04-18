using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _oder = 0;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _SceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utill.GetOnAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _oder;
            _oder++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = GameManager.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
        {
            go.transform.SetParent(parent);
        }
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Utill.GetOnAddComponent<T>(go);
    }
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = GameManager.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
        {
            go.transform.SetParent(parent);
        }
        return Utill.GetOnAddComponent<T>(go);
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = GameManager.Resource.Instantiate($"UI/Scene/{name}");
        T SceneUI = Utill.GetOnAddComponent<T>(go);
        _SceneUI = SceneUI;

        GameObject root = GameObject.Find("@UI_Root");

        go.transform.SetParent(Root.transform);

        return SceneUI;
    }
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = GameManager.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utill.GetOnAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }


    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!...");
            return;
        }
        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        GameManager.Resource.Destroy(popup.gameObject);
        popup = null;

    }
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
    public void Clear()
    {
        ClosePopupUI();
        _SceneUI = null;
    }
}
