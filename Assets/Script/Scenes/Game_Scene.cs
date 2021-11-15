using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Scene : Base_Scene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game; // 씬타입 지정

        GameManager.UI.ShowSceneUI<UI_Inven>(); // 유아이 생성

        Dictionary<int, Data.Stat> dict = GameManager.Data.StatDict; // 스텟데이터 받기

        gameObject.GetOrAddComponent<CursorController>(); // 커서 컴포넌트 추가

        GameObject player = GameManager.Game.Spwan(Define.WorldObject.Player, "UnityChan");// 플레이어 지정
        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);// 카메라를 플레이어에 붙여넣기

        GameObject go = new GameObject { name = "SpawningPool" }; // 스포닝풀 가져오기.
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>(); // ??
        pool.SetKeepMonsterCount(5); // 최대값 지정
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}