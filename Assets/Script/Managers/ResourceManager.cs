using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int Index = name.LastIndexOf('/');
            if (Index >= 0)
            {
                name = name.Substring(Index + 1);


                GameObject gb = GameManager.Pool.GetOriginal(name);
                if (gb != null)
                    return gb as T;
            }
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parents = null)
    {
        GameObject Original = Load<GameObject>($"Prefabs/{path}");

        if (Original == null)
        {
            Debug.Log($"Faild to Load Prefans : {path}");
            return null;
        }

        if (Original.GetComponent<Poolable>() != null)
            return GameManager.Pool.Pop(Original, parents).gameObject;

        GameObject go = Object.Instantiate(Original, parents);
        go.name = Original.name;

        return go;
    }

    public void Destroy(GameObject Go)
    {
        if (Go == null)
            return;

        Poolable poolable = Go.GetComponent<Poolable>();
        if (poolable != null)
        {
            GameManager.Pool.Push(poolable);
            return;
        }

        Object.Destroy(Go);
    }
}
