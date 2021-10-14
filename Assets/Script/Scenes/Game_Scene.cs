using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Scene : Base_Scene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        GameManager.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = GameManager.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = GameManager.Game.Spwan(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);

        GameObject go = new GameObject { name = "SpawningPool" }; // 스포닝푸 가져오기.
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>(); // ??
        pool.SetKeepMonsterCount(5); // 최대값 지정
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}