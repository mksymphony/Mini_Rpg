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
            // ������ üũ
            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (GameManager.Data.StatDict.TryGetValue(level + 1, out stat) == false) // ����ó�� ���� ������ ������
                    break;
                if (_exp < stat.TotalEXP)// Total EXP �� ���ڶ���.
                    break;
                level++;
            }
            if(level != Level) // ���� �� ���� ������ �ٸ���
            {
                Debug.Log("LEVEL UP!"); 
                Level = level; // �������� �ٽ� �������ش�.
                SetStat(Level); // ���� �й� ����
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
    public void SetStat(int Level) // ���� ���� �������� �޾� �������ش�.
    {
        Dictionary<int, Data.Stat> Dict = GameManager.Data.StatDict; // Json ���Ͽ� �ִ� �������� ���� ������ Dictionary
        Data.Stat stat = Dict[Level];   //�Ҵ�� ���� ������ �°� �й����ش�.

        _hp = stat.MaxHp;
        _maxhp = stat.MaxHp;
        _attack = stat.Attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
