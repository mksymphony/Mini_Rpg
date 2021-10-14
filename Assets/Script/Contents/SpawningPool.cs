using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _MonsterCount = 0; // ���� ���� ��
    [SerializeField]
    int _KeepMonsterCount = 0; // ������ ���� ��

    int _ReserveCount = 0; // ��Ȱ ī��Ʈ

    [SerializeField]
    Vector3 _SpawnPos; // ������ ��ġ
    [SerializeField]
    float _SpawnRidius = 15f; // ���� ������ ����
    [SerializeField]
    float _SpawnTime = 10.0f; // ���� ������ų �ð�.

    public void AddMonsterCount(int value) { _MonsterCount += value; } // ���� �� ���� �޴� �Լ� �߰�
    public void SetKeepMonsterCount(int count) { _KeepMonsterCount += count; }// ������ ���Ͱ��� �޴� �Լ�

    void Start()
    {
        GameManager.Game.OnspawnEvent -= AddMonsterCount; // ������
        GameManager.Game.OnspawnEvent += AddMonsterCount; // ���� �߰��� + �Ǵ� Action���� �ޱ����� ������.
    }

    void Update()
    {
        while (_ReserveCount + _MonsterCount < _KeepMonsterCount) // ���� ���� ���� �����Ǿ���� ���ͺ��� ������ ���ѹݺ�.
        {
            StartCoroutine("ReserveSpawn"); // �ڷ�ƾ ����.
        }
    }

    IEnumerator ReserveSpawn()
    {
        _ReserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _SpawnTime)); // 0 ~ 10f �ð��� �������� �����Ͽ� ����
        GameObject obj = GameManager.Game.Spwan(Define.WorldObject.Monster, "MaleDummy"); // ���� ���� ����.
        NavMeshAgent nav = obj.GetComponent<NavMeshAgent>(); // ������Ʈ �ִ� Navmesh�� ������.

        Vector3 randPos;

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _SpawnRidius); // �������� ������ ���� �� ���� �Ÿ�
            randDir.y = 0; // ���Ʒ��� �����ʵ��� ����ó��
            randPos = _SpawnPos + randDir; // ������ ������ġ���� ���� ������ ����.

            // ���� ������ �����ΰ�.
            NavMeshPath path = new NavMeshPath();
            if (nav.CalculatePath(randPos, path)) // ���� ���� ��ġ�Ͻ� ��ġ �籸��.
                break;

        }
        obj.transform.position = randPos;
        _ReserveCount--;
    }
}
