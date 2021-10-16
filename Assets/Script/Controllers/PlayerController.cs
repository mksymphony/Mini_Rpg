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
        WorldObjectType = Define.WorldObject.Player; //플레이어 타입 결정
        _stat = GetComponent<PlayerStat>();//플레이어 스테이트Json 파일값을 불러드림.
        GameManager.Input.MouseAction -= OnMouseEvent; // 예외처리 플레이어 마우스 이벤트를 취소시킴
        GameManager.Input.MouseAction += OnMouseEvent; // 플레이어 마우스 이벤트를 연결시킴.
        if (gameObject.GetComponentInChildren<UI_HP_Bar>() == null) //플레이어 HP바를 불러드림.
            GameManager.UI.MakeWorldSpaceUI<UI_HP_Bar>(transform); // 
    }
    protected override void UpdateMoving()
    {
        if (_lockTarget != null) // 타겟이 있을시.
        {
            _DestPos = _lockTarget.transform.position; // 타겟 위치값을 받아드림
            float distance = (_DestPos - transform.position).magnitude; // 타겟의 위치와 내 위치를 반환
            if (distance <= 1) // 거리가 1 과 같거나 이상일시.
            {
                State = Define.State.Skill; // 공격 시작
                return;
            }
        }

        Vector3 Dir = _DestPos - transform.position; // 타겟 과 내위치를 뺀 값.
        Dir.y = 0;// 예외처리 
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
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Dir, 1.0f, LayerMask.GetMask("Block"))) // 오브젝트 확인.
            {
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idel; // 강제로 스테이트를 바꾼다.
                return;
            }
            float movdDist = Mathf.Clamp(_stat.PlayerSpeed * Time.deltaTime, 0, Dir.magnitude);
            transform.position += Dir.normalized * movdDist;
            transform.LookAt(_DestPos);
        }
    }
    protected override void UpdateSkill()
    {
        if (_lockTarget != null) // 적을 발견할시.
        {
            Vector3 dir = _lockTarget.transform.position = transform.position; // 타겟의 위치값
            Quaternion quat = Quaternion.LookRotation(dir); // 적이 존재하는 방향
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime); // 플레이어가 바라보는 방향을 고정시켜줌
        }
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //공격 함수 호출.
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
