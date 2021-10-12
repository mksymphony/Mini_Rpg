using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;


    [SerializeField]
    float _scanRange = 3f;

    [SerializeField]
    float _attackRange = 1f;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = GetComponent<Stat>();
        if (gameObject.GetComponentInChildren<UI_HP_Bar>() == null)
            GameManager.UI.MakeWorldSpaceUI<UI_HP_Bar>(transform);
    }

    protected override void UpdateIdel()
    {
        GameObject player = GameManager.Game.GetPlayer();
        if (player == null)
            return;
        float distance = (player.transform.position - transform.position).magnitude;

        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }
    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _DestPos = _lockTarget.transform.position;
            float distance = (_DestPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
                nav.SetDestination(transform.position);
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
            NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
            nav.SetDestination(_DestPos);
            nav.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Dir), 20 * Time.deltaTime);
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

            targetStat.OnAttacked(_stat); //공격 함수 호출.

            if (targetStat.HP > 0)
            {
                float distance = (_DestPos - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idel;
            }
        }
        else
        {
            State = Define.State.Idel;
        }
    }
    /* protected virtual void UpdateDie()
     {
         Debug.Log("Monster UpdateDie");
     }
     */
}
