using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.Camera_Mode _Mode = Define.Camera_Mode.QuarterView;
    [SerializeField]
    Vector3 _Delta = new Vector3(0.0f, 5.0f, -5.0f);
    [SerializeField]
    GameObject _Player = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_Mode == Define.Camera_Mode.QuarterView)
        {

            if (_Player == null)
            {
                return;
            }
            RaycastHit hit;

            if (Physics.Raycast(_Player.transform.position, _Delta, out hit, _Delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float Dist = (hit.point - _Player.transform.position).magnitude * 0.8f;
                transform.position = _Player.transform.position + Vector3.up + _Delta.normalized * Dist;
            }
            else
            {
                transform.position = _Player.transform.position + _Delta;
                transform.LookAt(_Player.transform);
            }

        }
    }
    public void SetQuarterView(Vector3 Delta)
    {
        _Mode = Define.Camera_Mode.QuarterView;
        _Delta = Delta;
    }
}
