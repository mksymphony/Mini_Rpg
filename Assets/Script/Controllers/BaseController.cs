using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _DestPos;

    [SerializeField]
    protected Define.State _State = Define.State.Idel;

    [SerializeField]
    protected GameObject _lockTarget;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public virtual Define.State State
    {
        get { return _State; }
        set
        {
            _State = value;
            Animator anim = GetComponent<Animator>();

            switch (value)
            {
                case Define.State.Die:

                    break;
                case Define.State.Idel:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("Attack", 0.1f);
                    break;

            }
        }
    }

    private void Start()
    {
        Init();
    }
    void Update()
    {
        switch (_State)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Idel:
                UpdateIdel();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }

    public abstract void Init();

    protected virtual void UpdateDie() { }
    protected virtual void UpdateIdel() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateSkill() { }

}
