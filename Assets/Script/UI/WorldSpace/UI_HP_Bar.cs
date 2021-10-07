using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HP_Bar : UI_Base
{


    enum GameObjects
    {
        HPBar,

    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Update()
    {
        Transform Parents = transform.parent;
        transform.position = Parents.position + Vector3.up * (Parents.GetComponent<Collider>().bounds.size.y);
    }
}
