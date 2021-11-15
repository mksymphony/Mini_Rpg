using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Texture2D _AttackIcon; //
    Texture2D _MoveIcon;   //

    enum CursorType // 커서타입 지정
    {
        None, // 
        Attack,  //
        Move, //

    }
    CursorType _cursortype = CursorType.None; // 초기 상태 지정

    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster)); // 마스크 값 체크 
    void Start()
    {
        _AttackIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Attack");    //아이콘 불러오기
        _MoveIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Move");        //
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // 레이케스트 사용 
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 50.0f, Color.red, 1.0f);
        RaycastHit hit;    // 
        if (Physics.Raycast(ray, out hit, 50.0f, _mask))  // 마스크가 히트 될시.
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster) // 히트된 값이 몬스터일시.
            {
                if (_cursortype != CursorType.Attack) // 커서 타입.
                {
                    Cursor.SetCursor(_AttackIcon, new Vector2(_AttackIcon.width / 5, 0), CursorMode.Auto); // 마우스 아이콘 크기
                    _cursortype = CursorType.Attack; // 커서 타입 실행
                }
            }
            else
            {
                if (_cursortype != CursorType.Move) // 아닐시. 움직임 마우스 커서
                {
                    Cursor.SetCursor(_MoveIcon, new Vector2(_MoveIcon.width / 3, 0), CursorMode.Auto); // 마우스 아이콘 크기
                    _cursortype = CursorType.Move; // 무브
                }
            }

        }
    }
}
