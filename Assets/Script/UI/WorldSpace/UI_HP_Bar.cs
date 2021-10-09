using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP_Bar : UI_Base
{


    enum GameObjects
    {
        HP_Bar,

    }
    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }
    
    private void Update()
    {
        Transform Parent = transform.parent;
        transform.position = Parent.position + Vector3.up * (Parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HP  / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HP_Bar).GetComponent<Slider>().value = ratio;
    }

    
}
