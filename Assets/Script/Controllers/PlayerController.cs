using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Local -> World = TransformDirection  로컬좌표에서 월드 좌표로.
//World -> Local = InverseTransformDirection 월드좌표에서 로컬 좌표로.

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));

    bool _StopSkill = false;

    PlayerStat _stat;
    public override void Init()
    {

        _stat = GetComponent<PlayerStat>();
        GameManager.Input.MouseAction -= OnMouseEvent;
        GameManager.Input.MouseAction += OnMouseEvent;
        if (gameObject.GetComponentInChildren<UI_HP_Bar>() == null)
            GameManager.UI.MakeWorldSpaceUI<UI_HP_Bar>(transform);
    }
    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _DestPos = _lockTarget.transform.position;
            float distance = (_DestPos - transform.position).magnitude;
            if (distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 Dir = _DestPos - transform.position;
        if (Dir.magnitude < 0.1f)
        {
            State = Define.State.Idel;
        }
        else
        {
            //  NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
            // float MoveDist = Mathf.Clamp(_stat.PlayerSpeed * Time.deltaTime, 0, Dir.magnitude);
            //  nav.Move(Dir.normalized * MoveDist);


            Debug.DrawRay(transform.position, Dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idel;
                return;
            }
            float movdDist = Mathf.Clamp(_stat.PlayerSpeed * Time.deltaTime, 0, Dir.magnitude);
            transform.position += Dir.normalized * movdDist;
            transform.LookAt(_DestPos);
        }
        /*      Animation       */

    }
    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position = transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();

            int Damege = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            Debug.Log(Damege);
            targetStat.HP -= Damege;

        }

        if (_StopSkill)
        {
            State = Define.State.Idel;
        }
        else
        {
            State = Define.State.Skill;
        }
    }
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idel:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _StopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool RaycaseHit = Physics.Raycast(ray, out hit, 50.0f, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (RaycaseHit)
                    {
                        _DestPos = hit.point;
                        State = Define.State.Moving;
                        _StopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Click:
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && RaycaseHit)
                        _DestPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _StopSkill = true;
                break;

        }
    }
}
