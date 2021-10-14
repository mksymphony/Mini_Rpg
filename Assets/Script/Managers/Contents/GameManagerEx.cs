using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    // int <<-->>  GameObject 
    //Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>(); // 플레이어가 한명뿐이기때문에 사용하지 않음.
    GameObject _player;

    HashSet<GameObject> _monster = new HashSet<GameObject>(); // ItemID를 관리할 필요가 없기때문에 Dictionary 대신 HashSet을 이용

    public Action<int> OnspawnEvent; // 리턴타입이 없는 Action을 이용 하여 값 전달
    public GameObject GetPlayer() { return _player; }
    public GameObject Spwan(Define.WorldObject type, string path, Transform parent = null) //스폰시킬 객체 타입, 경로 , 부모 를 정의
    {
        GameObject go = GameManager.Resource.Instantiate(path, parent); //게임오브젝트 go 에 생성할 객체의 경로,부모를 받아서 저장함

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monster.Add(go);            // 받는 형식이 몬스터일시 몬스터를 정의
                if (OnspawnEvent != null) // 몬스터가 추가될시 값 1추가
                    OnspawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;                // 플레이어 일시 플레이어를 정의
                break;
            case Define.WorldObject.Unknown:
                break;                       // 없을시 빠져나감
        }
        return go;
    }
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>(); // 타입의 형식을 확인하기 위해 BaseController의 형식을 확인

        if (bc == null)  // 비어있을시 Unknown 으로 지정
            return Define.WorldObject.Unknown;


        return bc.WorldObjectType;  // 컨트롤러 타입을 반환
    }

    public void DeSpawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);  // 컨트롤러 타입을 받음.


        switch (type)
        {
            case Define.WorldObject.Monster:  //몬스터 타입을 초기화 시킴
                {
                    if (_monster.Contains(go))
                    {
                        _monster.Remove(go);
                        if (OnspawnEvent != null) // 몬스터가 추가될시 값 -1추가
                            OnspawnEvent.Invoke(-1);
                    }
                    break;
                }
            case Define.WorldObject.Player:
                {
                    if (_player == go)  // 플레이어를 초기화시킴
                        _player = null;
                    break;
                }
        }
        GameManager.Resource.Destroy(go); // 게임 오브젝트 삭제.

    }
}
