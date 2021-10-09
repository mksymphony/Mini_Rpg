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
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}