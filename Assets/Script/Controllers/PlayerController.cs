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


    public enum PlayerState
    {
        Idel,
        Moving,
        Die,
        Skill,
    }
    void Start()
    {


        _stat = GetComponent<PlayerStat>();
        /* GameManager.Input.KeyAction -= onKeyboard;
        GameManager.Input.KeyAction += onKeyboard; */
        GameManager.Input.MouseAction -= OnMouseEvent;
        GameManager.Input.MouseAction += OnMouseEvent;
    }

    [SerializeField]
    PlayerState _State = PlayerState.Idel;

    public PlayerState State
    {
        get { return _State; }
        set
        {
            _State = value;
            Animator anim = GetComponent<Animator>();

            switch (value)
            {
                case PlayerState.Die:
                    anim.SetBool("Attack", false);
                    break;
                case PlayerState.Idel:
                    anim.SetFloat("Speed", 0);
                    anim.SetBool("Attack", false);
                    break;
                case PlayerState.Moving:
                    anim.SetFloat("Speed", _stat.MoveSpeed);
                    anim.SetBool("Attack", false);
                    break;
                case PlayerState.Skill:
                    anim.SetBool("Attack", true);
                    break;

            }
        }
    }

    void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            float distance = (_DestPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        Vector3 Dir = _DestPos - transform.position;
        if (Dir.magnitude < 0.1f)
        {
            State = PlayerState.Idel;
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
                    State = PlayerState.Idel;
                return;
            }

            transform.LookAt(_DestPos);
        }
        /*      Animation       */

    }
    void UpdateDie()
    {

    }
    void UpdateSkill()
    {

    }
    void UpdateIdel()
    {

    }
    void OnHitEvent()
    {
        State = PlayerState.Moving;
    }

    void Update()
    {
        switch (_State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idel:
                UpdateIdel();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Skill:
                UpdateSkill();
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

    GameObject _lockTarget;
    int _mask = (1 << (int)Defind.Layer.Ground | (1 << (int)Defind.Layer.Monster));
    void OnMouseEvent(Defind.MouseEvent evt)
    {
        if (State == PlayerState.Die)
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
                        State = PlayerState.Moving;
                        if (hit.collider.gameObject.layer == (int)Defind.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
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
