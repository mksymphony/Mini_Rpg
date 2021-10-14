using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _MonsterCount = 0; // 현재 몬스터 수
    [SerializeField]
    int _KeepMonsterCount = 0; // 유지할 몬스터 수

    int _ReserveCount = 0; // 부활 카운트

    [SerializeField]
    Vector3 _SpawnPos; // 스폰할 위치
    [SerializeField]
    float _SpawnRidius = 15f; // 랜덤 스폰할 범위
    [SerializeField]
    float _SpawnTime = 10.0f; // 랜덤 스폰시킬 시간.

    public void AddMonsterCount(int value) { _MonsterCount += value; } // 몬스터 수 값을 받는 함수 추가
    public void SetKeepMonsterCount(int count) { _KeepMonsterCount += count; }// 유지할 몬스터값을 받는 함수

    void Start()
    {
        GameManager.Game.OnspawnEvent -= AddMonsterCount; // 안전빵
        GameManager.Game.OnspawnEvent += AddMonsterCount; // 몬스터 추가시 + 되는 Action값을 받기위해 연결함.
    }

    void Update()
    {
        while (_ReserveCount + _MonsterCount < _KeepMonsterCount) // 현재 몬스터 값이 유지되어야할 몬스터보다 낮을시 무한반복.
        {
            StartCoroutine("ReserveSpawn"); // 코루틴 실행.
        }
    }

    IEnumerator ReserveSpawn()
    {
        _ReserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _SpawnTime)); // 0 ~ 10f 시간중 랜덤으로 선택하여 실행
        GameObject obj = GameManager.Game.Spwan(Define.WorldObject.Monster, "MaleDummy"); // 더미 몬스터 생성.
        NavMeshAgent nav = obj.GetComponent<NavMeshAgent>(); // 오브젝트 있는 Navmesh를 추출함.

        Vector3 randPos;

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _SpawnRidius); // 랜덤으로 설정된 방향 과 랜덤 거리
            randDir.y = 0; // 땅아래가 되지않도록 예외처리
            randPos = _SpawnPos + randDir; // 지정된 스폰위치에서 랜덤 구역을 지정.

            // 스폰 가능한 지역인가.
            NavMeshPath path = new NavMeshPath();
            if (nav.CalculatePath(randPos, path)) // 갈수 없는 위치일시 위치 재구성.
                break;

        }
        obj.transform.position = randPos;
        _ReserveCount--;
    }
}
