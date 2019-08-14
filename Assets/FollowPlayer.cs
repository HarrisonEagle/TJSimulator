using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
        //offset.y = 0.5f;
        //offset.z = 7f;

    }  // ターゲットへの参照

    void Update()
    {
        // 自分の座標にtargetの座標を代入する
        transform.position = player.transform.position + offset;
    }
}


