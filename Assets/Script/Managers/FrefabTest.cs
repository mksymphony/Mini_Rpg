using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrefabTest : MonoBehaviour
{
    GameObject Prefab;

    GameObject Tank;

    void Start()
    {
        Tank = GameManager.Resource.Instantiate("Tank");

        Destroy(Tank, 3.0f);
    }
}
