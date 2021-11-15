using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Base_Scene : MonoBehaviour
{
    Define.Scene _SceneType = Define.Scene.Game;  //초기 씬의 상태

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Lobby;
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem)); //이벤트 시스템 지정.

        if (obj == null) //비어있을시 생성.
        {
            GameManager.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }
    public abstract void Clear();

}
