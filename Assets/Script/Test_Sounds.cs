using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Sounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public AudioClip audio1;
    public AudioClip audio2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {

        i++;
        if (i % 2 == 0)
            GameManager.Sound.Play(audio1, Define.Sound.BGM);
        else
            GameManager.Sound.Play(audio2, Define.Sound.Effect);

    }
}
