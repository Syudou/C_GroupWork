using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーオブジェクトを検索　
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //もしもプレイヤーがいれば処理する
        if (player != null)
        {
            //プレイヤーの位置と連動させる
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
