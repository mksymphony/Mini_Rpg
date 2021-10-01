using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //Local -> World = TransformDirection  로컬좌표에서 월드 좌표로.
    //World -> Local = InverseTransformDirection 월드좌표에서 로컬 좌표로.

    PlayerStat _stat;
    Vector3 _DestPos;

    Texture2D _AttackIcon;
    Texture2D _MoveIcon;

    enum CursorType
    {
        None,
        Attack,
        Move,

    }
    CursorType _cursortype = CursorType.None;

    public enum PlayerState
    {
        Idel,
        Moving,
        Die,
        Skill,
    }
    void Start()
    {
        _AttackIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _MoveIcon = GameManager.Resource.Load<Texture2D>("Textures/Cursor/Move");

        _stat = GetComponent<PlayerStat>();
        /* GameManager.Input.KeyAction -= onKeyboard;
        GameManager.Input.KeyAction += onKeyboard; */
        GameManager.Input.MouseAction -= OnMouseEvent;
        GameManager.Input.MouseAction += OnMouseEvent;
    }

    PlayerState _State = PlayerState.Idel;
    void UpdateMoving()
    {

        Vector3 Dir = _DestPos - transform.position;
        if (Dir.magnitude < 0.1f)
        {
            _State = PlayerState.Idel;
        }
        else
        {
            NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
            float MoveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, Dir.magnitude);
            //  nav.CalculatePath;
            nav.Move(Dir.normalized * MoveDist);
            // nav.SetDestination(_DestPos);

            Debug.DrawRay(transform.position, Dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    _State = PlayerState.Idel;
                return;
            }

            transform.LookAt(_DestPos);
        }
        /*      Animation       */
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", _stat.MoveSpeed);
    }

    void UpdateIdel()
    {
        /*      Animation       */
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 0);
    }
    void UpdateDie()
    {

    }

    void UpdateMouseCursor()
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
    void Update()
    {
        UpdateMouseCursor();
        switch (_State)
        {
            case PlayerState.Idel:
                UpdateIdel();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;

        }
    }


    /*void onKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
            transform.position += (Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);
            transform.position += (Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);
            transform.position += (Vector3.right * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);
            transform.position += (Vector3.left * Time.deltaTime * _speed);
        }
        _MoveToDest = false;
    }
    */

    int _mask = (1 << (int)Defind.Layer.Ground | (1 << (int)Defind.Layer.Monster));
    GameObject _lockTarget;
    void OnMouseEvent(Defind.MouseEvent evt)
    {
        if (_State == PlayerState.Die)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool RaycaseHit = Physics.Raycast(ray, out hit, 50.0f, _mask);

        switch (evt)
        {
            case Defind.MouseEvent.PointerDown:
                {
                    if (RaycaseHit)
                    {
                        _DestPos = hit.point;
                        _State = PlayerState.Moving;
                        if (hit.collider.gameObject.layer == (int)Defind.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Defind.MouseEvent.PointerUp:
                _lockTarget = null;
                break;
            case Defind.MouseEvent.Click:
                break;
            case Defind.MouseEvent.Press:
                {
                    if (_lockTarget != null)
                    {
                        _DestPos = _lockTarget.transform.position;
                    }
                    else if (RaycaseHit)
                        _DestPos = hit.point;
                }
                break;
        }
    }
}
