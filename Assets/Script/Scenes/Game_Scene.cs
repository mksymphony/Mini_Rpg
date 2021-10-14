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

        GameObject go = new GameObject { name = "SpawningPool" }; // ������Ǫ ��������.
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>(); // ??
        pool.SetKeepMonsterCount(5); // �ִ밪 ����
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}