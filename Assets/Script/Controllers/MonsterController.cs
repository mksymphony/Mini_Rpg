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
        _stat = GetComponent<Stat>();
        if (gameObject.GetComponentInChildren<UI_HP_Bar>() == null)
            GameManager.UI.MakeWorldSpaceUI<UI_HP_Bar>(transform);
    }

    protected override void UpdateIdel()
    {
        Debug.Log("Monster UpdateIdle");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;
        float distance = (player.transform.position - transform.position).magnitude;

        if (distance <= _scanRange)
        {
            Debug.Log("ScanTarget");

            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }
    protected override void UpdateMoving()
    {
        Debug.Log("Monster UpdateMoving");
        if (_lockTarget != null)
        {
            _DestPos = _lockTarget.transform.position;
            float distance = (_DestPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
                nav.SetDestination(transform.position);
                Debug.Log("MonsterAttack");
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
        Debug.Log("Monster OnHitEvent");
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            Stat myStat = gameObject.GetComponent<Stat>();

            int Damege = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            Debug.Log(Damege);
            targetStat.HP -= Damege;

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
