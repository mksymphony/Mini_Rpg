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
        _depense = 2;
        _speed = 5.0f;
    }
    public virtual void OnAttacked(Stat attacker)  // 공격자의 스텟을 가져와 공격했을때를 확인.
    {
        int Damege = Mathf.Max(0, attacker.Attack - Defense); // Damege 는 Mathf 함수의 최대값 을 사용하여 0 ~ 공격자의 최대 공격력 에서 피공격자의 방어력을 뺀다.
        HP -= Damege;  // 최종적으로 경감된 공격력을 피공격자의 HP에서 뺀다.

        if (HP <= 0) // HP 가 음수가 안되도록 강제 조정
        {
            HP = 0;
            OnDead(attacker); // 대상의 체력이 0 OR 음수일시 OnDead 함수 호출
        }
    }
    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat; // 공격자 형태를 플레이어로 형변환
        if (playerStat != null) // 예외 처리
        {
            playerStat.Exp += 20;
        }
        GameManager.Game.DeSpawn(gameObject); // 대상을 게임에서 제거.
    }
}
