using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static c_GameManager;

public class c_PlayerController : MonoBehaviour
{
    public float speed = 3.0f;          //移動スピード
    int direction = 0;                  //移動方向
    float axisH;                        //横軸
    float axisV;                        //縦軸
    public float angleZ = -90.0f;       //回転角度

    Rigidbody2D rbody;                  //Rigidbody2D
    Animator animator;                  //Animator
    private Vector2 movement;
    //アレンジ
    SpriteRenderer sprite; //SpriteRendererを参照予定


    bool isMoving = false;              //移動中フラグ

    //
    public static int hp = 1;       //
    public static string gameState; //
    bool inDamage = false;          //

    //ｐ１からｐ２の角度を返す 
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            
            float rad = Mathf.Atan2(dy, dx);
            
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            
            angle = angleZ;

        }
        return angle;
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();    //Rigidbody2Dを得る
        animator = GetComponent<Animator>();    //Animatorを得る

        ////アレンジ
        //sprite = GetComponent<SpriteRenderer>(); //PlayerのSpriteRendererを参照


        //
        c_GameManager.CurrentState = GameState.Playing;
        gameState = "playing";

        //HP更新
        hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム状態がPlayingでない場合は動きを止める
        if (c_GameManager.CurrentState != c_GameManager.GameState.Playing)
        {
            movement = Vector2.zero; // 移動ベクトルをリセット
            rbody.velocity = Vector2.zero;
            return; // Update処理を終了
        }

        else
        {
            if (gameState != "playing" || inDamage)
            {
                return;
            }

            if (isMoving == false)
            {
                axisH = Input.GetAxisRaw("Horizontal");         //左右キー入力
                axisV = Input.GetAxisRaw("Vertical");           //上下キー入力
            }
            //キー入力から移動角度を求める
            Vector2 fromPt = transform.position;
            Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
            angleZ = GetAngle(fromPt, toPt);

            //移動角度から向いている方向とアニメーション更新
            int dir;
            if (angleZ >= -45 && angleZ < 45)
            {
                //右向き
                dir = 3;
            }
            else if (angleZ >= 45 && angleZ <= 135)
            {
                //上向き
                dir = 2;
            }
            else if (angleZ >= -135 && angleZ <= -45)
            {
                //下向き
                dir = 0;
            }
            else
            {
                //左向き
                dir = 1;
            }
            if (dir != direction)
            {
                direction = dir;
                animator.SetInteger("Direction", direction);
            }



        }
        ////
        //gameState = "playing";
    }

    void FixedUpdate()
    {
        Debug.Log(inDamage);

        if (c_GameManager.CurrentState != c_GameManager.GameState.Playing)
        {
            // ゲームクリアまたはゲームオーバー時に速度を完全にリセット
            rbody.velocity = Vector2.zero;
            return;
        }

        //
        if (gameState != "playing")
        {
            return;
        }

        if (inDamage)
        {
            //
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0)
            {
                //
                //gameObject.GetComponent<SpriteRenderer>().enabled = true;
                sprite.enabled = true;
            }
            else
            {
                //
                //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                sprite.enabled = false;
            }

            return; //
        }


        //移動速度を更新する
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    //バーチャルパッド用
    public void SetAxis(float h, float v)
    {
        axisH = h;
        axisV = v;
        if (axisH == 0 && axisV == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    //
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("collsion");

            //ダメージ中であれば発動しない※無敵時間
            if (!inDamage)
            {
                //ダメージを受ける自作メソッド
                GetDamage(collision.gameObject);
                

            }
        }
    }

    //ダメージをうけるメソッド
    void GetDamage(GameObject enemy)
    {
        if (gameState == "playing")
        {
            hp--; //HPを減らす
            

            if (hp > 0)
            {
                //移動を一旦停止
                rbody.velocity = new Vector2(0, 0);
                //ぶつかった相手のいる方向と反対方向を割り出す
                Vector3 v = (transform.position - enemy.transform.position).normalized;

                //割り出した反対方向にヒットバックさせる
                rbody.AddForce(new Vector2(v.x * 4, v.y * 4), ForceMode2D.Impulse);

                //ダメージ受け中のフラグをON
                inDamage = true;

                //時間差でダメージ受け中のフラグを解除
                Invoke("DamageEnd", 0.25f);

            }
            else
            {
                //
                GameOver();
            }
        }
    }


    //
    void DamageEnd()
    {
        inDamage = false;                                           
        gameObject.GetComponent<SpriteRenderer>().enabled = true;   
    }
    //
    void GameOver()
    {
        gameState = "gameover";
        c_GameManager.CurrentState = GameState.GameOver;


        GetComponent<CircleCollider2D>().enabled = false;          
        rbody.velocity = new Vector2(0, 0);                       
        rbody.gravityScale = 1;                                     
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);     
        animator.SetBool("IsDead", true);                           
        Destroy(gameObject, 1.0f);                                  
    }

}
