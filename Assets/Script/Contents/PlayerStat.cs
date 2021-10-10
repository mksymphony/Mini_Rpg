using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    protected float _PlayerSpeed;

    public int Exp { get { return _exp; } set { _exp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public float PlayerSpeed { get { return _PlayerSpeed; } set { _PlayerSpeed = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 200;
        _maxhp = 200;
        _attack = 10;
        _depense = 5;
        _speed = 2.0f;
        _PlayerSpeed = 5.0f;
        _exp = 0;
        _gold = 100;
    }

}
