using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance; // ����ƽ�� Ư���� �̿��� ���ӸŴ����� ���ϼ��� �������ش�.
    static GameManager Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� �����´�.



    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    SceneManagerEX _Scene = new SceneManagerEX();
    public static SceneManagerEX Scene { get { return Instance._Scene; } }

    SoundManager _Sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._Sound; } }

    UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }


    void Start()
    {
        //�ʱ�ȭ
        Init();
    }
    void Update()
    {
        Input.OnUpdate();

    }
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject GO = GameObject.Find("@GameManagers");
            if (GO == null)
            {
                GO = new GameObject { name = "@GameManagers" };
                GO.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(GO);
            s_instance = GO.GetComponent<GameManager>();


            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._Sound.Init();
        }
    }
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
