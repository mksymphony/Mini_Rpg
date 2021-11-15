using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Scene : Base_Scene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game; // ��Ÿ�� ����

        GameManager.UI.ShowSceneUI<UI_Inven>(); // ������ ����

        Dictionary<int, Data.Stat> dict = GameManager.Data.StatDict; // ���ݵ����� �ޱ�

        gameObject.GetOrAddComponent<CursorController>(); // Ŀ�� ������Ʈ �߰�

        GameObject player = GameManager.Game.Spwan(Define.WorldObject.Player, "UnityChan");// �÷��̾� ����
        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);// ī�޶� �÷��̾ �ٿ��ֱ�

        GameObject go = new GameObject { name = "SpawningPool" }; // ������Ǯ ��������.
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>(); // ??
        pool.SetKeepMonsterCount(5); // �ִ밪 ����
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}