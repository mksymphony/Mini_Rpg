using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Base_Scene : MonoBehaviour
{
    Define.Scene _SceneType = Define.Scene.Game;  //�ʱ� ���� ����

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Lobby;
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem)); //�̺�Ʈ �ý��� ����.

        if (obj == null) //��������� ����.
        {
            GameManager.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }
    public abstract void Clear();

}
