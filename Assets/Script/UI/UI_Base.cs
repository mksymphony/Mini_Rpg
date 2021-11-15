using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

	public abstract void Init();

    private void Start()
    {
		Init();
    }
    protected void Bind<T>(Type type) where T : UnityEngine.Object // 게임오브젝트 타입 묶어두기.
	{
		string[] names = Enum.GetNames(type); // 타입의 이름을 스트링 배열 형식으로 저장해둔다.
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // 오브젝트에 이름크기만큼 받아온다.
		_objects.Add(typeof(T), objects); // 오브젝트 추가.

		for (int i = 0; i < names.Length; i++) // 이름의 길이 만큼.
		{
			if (typeof(T) == typeof(GameObject)) // 받아온 오브젝트가 게임오브젝트이리.
				objects[i] = Utill.FindChild(gameObject, names[i], true); // 자식 받아오기.
			else
				objects[i] = Utill.FindChild<T>(gameObject, names[i], true); // 일반 타입 자식 받아오기

			if (objects[i] == null) // 예외처리.
				Debug.Log($"Failed to bind({names[i]})");
		}
	}
	protected T Get<T>(int idx) where T : UnityEngine.Object // 오브젝트 형식 인덱스 받아오기.
	{
		UnityEngine.Object[] objects = null; // 
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected Text GetText(int idx) { return Get<Text>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }

	public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
	{
		UI_EventHandler evt = Utill.GetOnAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				evt.OnDragHandler += action;
				break;
		}
	}
}
