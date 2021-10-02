using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Texture2D _AttackIcon;
    Texture2D _MoveIcon;

    enum CursorType
    {
        None,
        Attack,
        Move,

    }
    CursorType _cursortype = CursorType.None;

    int _mask = (1 << (int)Defind.Layer.Ground | (1 << (int)Defind.Layer.Monster));
    void Start()
    {
        _AttackIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _MoveIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Move");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 50.0f, Color.red, 1.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Defind.Layer.Monster)
            {
                if (_cursortype != CursorType.Attack)
                {
                    Cursor.SetCursor(_AttackIcon, new Vector2(_AttackIcon.width / 5, 0), CursorMode.Auto);
                    _cursortype = CursorType.Attack;
                }
            }
            else
            {
                if (_cursortype != CursorType.Move)
                {
                    Cursor.SetCursor(_MoveIcon, new Vector2(_MoveIcon.width / 3, 0), CursorMode.Auto);
                    _cursortype = CursorType.Move;
                }
            }

        }
    }
}
