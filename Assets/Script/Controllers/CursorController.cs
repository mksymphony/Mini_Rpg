using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Texture2D _AttackIcon; //
    Texture2D _MoveIcon;   //

    enum CursorType // Ŀ��Ÿ�� ����
    {
        None, // 
        Attack,  //
        Move, //

    }
    CursorType _cursortype = CursorType.None; // �ʱ� ���� ����

    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster)); // ����ũ �� üũ 
    void Start()
    {
        _AttackIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Attack");    //������ �ҷ�����
        _MoveIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Move");        //
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // �����ɽ�Ʈ ��� 
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 50.0f, Color.red, 1.0f);
        RaycastHit hit;    // 
        if (Physics.Raycast(ray, out hit, 50.0f, _mask))  // ����ũ�� ��Ʈ �ɽ�.
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster) // ��Ʈ�� ���� �����Ͻ�.
            {
                if (_cursortype != CursorType.Attack) // Ŀ�� Ÿ��.
                {
                    Cursor.SetCursor(_AttackIcon, new Vector2(_AttackIcon.width / 5, 0), CursorMode.Auto); // ���콺 ������ ũ��
                    _cursortype = CursorType.Attack; // Ŀ�� Ÿ�� ����
                }
            }
            else
            {
                if (_cursortype != CursorType.Move) // �ƴҽ�. ������ ���콺 Ŀ��
                {
                    Cursor.SetCursor(_MoveIcon, new Vector2(_MoveIcon.width / 3, 0), CursorMode.Auto); // ���콺 ������ ũ��
                    _cursortype = CursorType.Move; // ����
                }
            }

        }
    }
}
