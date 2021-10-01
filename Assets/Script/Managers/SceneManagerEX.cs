using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public Base_Scene CurrentScene
    {
        get { return GameObject.FindObjectOfType<Base_Scene>(); }
    }

    public void LoadScene(Defind.Scene Type)
    {
        GameManager.Clear();
        SceneManager.LoadScene(GetSceneName(Type));
    }
    string GetSceneName(Defind.Scene Type)
    {
        string name = System.Enum.GetName(typeof(Defind.Scene), Type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
