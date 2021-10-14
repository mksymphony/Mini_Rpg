using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    // int <<-->>  GameObject 
    //Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>(); // �÷��̾ �Ѹ���̱⶧���� ������� ����.
    GameObject _player;

    HashSet<GameObject> _monster = new HashSet<GameObject>(); // ItemID�� ������ �ʿ䰡 ���⶧���� Dictionary ��� HashSet�� �̿�

    public Action<int> OnspawnEvent; // ����Ÿ���� ���� Action�� �̿� �Ͽ� �� ����
    public GameObject GetPlayer() { return _player; }
    public GameObject Spwan(Define.WorldObject type, string path, Transform parent = null) //������ų ��ü Ÿ��, ��� , �θ� �� ����
    {
        GameObject go = GameManager.Resource.Instantiate(path, parent); //���ӿ�����Ʈ go �� ������ ��ü�� ���,�θ� �޾Ƽ� ������

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monster.Add(go);            // �޴� ������ �����Ͻ� ���͸� ����
                if (OnspawnEvent != null) // ���Ͱ� �߰��ɽ� �� 1�߰�
                    OnspawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;                // �÷��̾� �Ͻ� �÷��̾ ����
                break;
            case Define.WorldObject.Unknown:
                break;                       // ������ ��������
        }
        return go;
    }
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>(); // Ÿ���� ������ Ȯ���ϱ� ���� BaseController�� ������ Ȯ��

        if (bc == null)  // ��������� Unknown ���� ����
            return Define.WorldObject.Unknown;


        return bc.WorldObjectType;  // ��Ʈ�ѷ� Ÿ���� ��ȯ
    }

    public void DeSpawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);  // ��Ʈ�ѷ� Ÿ���� ����.


        switch (type)
        {
            case Define.WorldObject.Monster:  //���� Ÿ���� �ʱ�ȭ ��Ŵ
                {
                    if (_monster.Contains(go))
                    {
                        _monster.Remove(go);
                        if (OnspawnEvent != null) // ���Ͱ� �߰��ɽ� �� -1�߰�
                            OnspawnEvent.Invoke(-1);
                    }
                    break;
                }
            case Define.WorldObject.Player:
                {
                    if (_player == go)  // �÷��̾ �ʱ�ȭ��Ŵ
                        _player = null;
                    break;
                }
        }
        GameManager.Resource.Destroy(go); // ���� ������Ʈ ����.

    }
}
