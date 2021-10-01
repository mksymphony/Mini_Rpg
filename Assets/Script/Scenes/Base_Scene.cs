using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Base_Scene : MonoBehaviour
{
    Defind.Scene _SceneType = Defind.Scene.Unknown;  //�ʱ� ���� ����

    public Defind.Scene SceneType { get; protected set; } = Defind.Scene.Unknown;
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
        {
            GameManager.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }
    public abstract void Clear();

}
