using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    Stat _stat;

    public override void Init()
    {
        _stat = GetComponent<Stat>();
        if (gameObject.GetComponentInChildren<UI_HP_Bar>() == null)
            GameManager.UI.MakeWorldSpaceUI<UI_HP_Bar>(transform);
    }

    protected override void UpdateIdel()
    {
        Debug.Log("Monster UpdateIdle");
    }
    protected override void UpdateMoving()
    {
        Debug.Log("Monster UpdateMoving");
    }
    protected override void UpdateSkill()
    {
        Debug.Log("Monster UpdateSkill");
    }
    void OnHitEvent()
    {
        Debug.Log("Monster OnHitEvent");
    }
    /* protected virtual void UpdateDie()
     {
         Debug.Log("Monster UpdateDie");
     }
     */
}
