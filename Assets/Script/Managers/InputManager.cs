using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _Pressed = false;
    float _PressedTime = 0;
    public void OnUpdate()
    {
        //    if (EventSystem.current.IsPointerOverGameObject())
        //     return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_Pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _PressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _Pressed = true;
            }
            else
            {
                if (_Pressed)
                {
                    if (Time.time < _PressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _Pressed = false;
                _PressedTime = 0;
            }
        }
    }
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
