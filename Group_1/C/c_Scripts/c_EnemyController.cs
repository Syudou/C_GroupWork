using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class c_EnemyController : MonoBehaviour
{
    GameObject Player;
    Vector3 PlayerPos;
    public static string gameState; //
    //private bool isGameOver = false;

    public float speed = 0.5f;

    Vector3 diff;
    Vector3 vector;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = Player.transform.position;
        transform.LookAt(PlayerPos);

    }
    void Update()
    {
        // ゲームオーバーなら処理を停止
        if (gameState == "gameover") return;
        

        PlayerPos = Player.transform.position;//プレイヤーの現在位置を取得
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos, speed * Time.deltaTime);//現在位置からプレイヤーの位置に向けて移動
            diff.x = PlayerPos.x - transform.position.x;//プレイヤーと敵キャラのX軸の位置関係を取得する
            if (diff.x > 0)
            {
                //Playerが敵キャラの右側にいる時右側を向く
                vector = new Vector3(0, -180, 0);
                transform.eulerAngles = vector;
            }
            if (diff.x < 0)
            {
                //Playerが敵キャラの左側にいる時左側を向く
                vector = new Vector3(0, 0, 0);
                transform.eulerAngles = vector;
            }
        
        
    }

    public void GameOver()
    {
        Debug.Log("Game Over!"); // コンソールにメッセージを表示
        // 必要に応じて、ゲームオーバー画面を表示するなどの処理を追加
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在のシーンをリロード
    }

    //public void StopMovement()
    //{
    //    isGameOver = true;
    //}

}
    

