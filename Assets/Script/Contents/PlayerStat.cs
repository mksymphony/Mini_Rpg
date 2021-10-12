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

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            // 레벨업 체크
            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (GameManager.Data.StatDict.TryGetValue(level + 1, out stat) == false) // 예외처리 다음 레벨이 없을시
                    break;
                if (_exp < stat.TotalEXP)// Total EXP 가 모자랄시.
                    break;
                level++;
            }
            if(level != Level) // 레벨 이 현재 레벨과 다를시
            {
                Debug.Log("LEVEL UP!"); 
                Level = level; // 레벨값을 다시 세팅해준다.
                SetStat(Level); // 스텟 분배 시작
            }
        }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public float PlayerSpeed { get { return _PlayerSpeed; } set { _PlayerSpeed = value; } }

    private void Start()
    {
        _level = 1;

        _depense = 5;
        _speed = 2.0f;
        _PlayerSpeed = 5.0f;
        _exp = 0;
        _gold = 0;

        SetStat(_level);
    }
    public void SetStat(int Level) // 스텟 세팅 레벨값을 받아 수정해준다.
    {
        Dictionary<int, Data.Stat> Dict = GameManager.Data.StatDict; // Json 파일에 있는 스텟정보 값을 저장할 Dictionary
        Data.Stat stat = Dict[Level];   //할당된 값을 레벨에 맞게 분배해준다.

        _hp = stat.MaxHp;
        _maxhp = stat.MaxHp;
        _attack = stat.Attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
