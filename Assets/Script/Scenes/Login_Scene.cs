using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login_Scene : Base_Scene
{

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            list.Add(GameManager.Resource.Instantiate("Unitychan"));
        }
        foreach (GameObject obj in list)
            GameManager.Resource.Destroy(obj);

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GameManager.Scene.LoadScene(Define.Scene.Game);
    }
    public override void Clear()
    {
        Debug.Log("Scene Clear");
    }
}
