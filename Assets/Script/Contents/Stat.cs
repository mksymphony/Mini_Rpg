using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;

    [SerializeField]
    protected int _hp;

    [SerializeField]
    protected int _maxhp;

    [SerializeField]
    protected int _attack;

    [SerializeField]
    protected int _depense;

    [SerializeField]
    protected float _speed;

    public int Level { get { return _level; } set { _level = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _depense; } set { _depense = value; } }
    public float MoveSpeed { get { return _speed; } set { _speed = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxhp = 100;
        _attack = 10;
        _depense = 5;
        _speed = 5.0f;

    }
}
