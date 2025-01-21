using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class c_Enemy3Controller : MonoBehaviour
{
    public float speed = 5f; // エネミーの移動速度
    public float lifetime = 5f; // エネミーが消えるまでの時間

    public static string gameState;

    private Vector3 direction; // 移動方向

    //private bool isGameOver = false;
    void Start()
    {
        if (gameState == "gameover") return;

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // プレイヤーの方向を計算
            direction = (player.transform.position - transform.position).normalized;
        }
        //else
        //{
        //    // 移動方向をランダムに設定
        //    float randomX = Random.Range(-1f, 1f);
        //    float randomY = Random.Range(-1f, 1f);
        //    direction = new Vector3(randomX, randomY, 0).normalized;
        //}

        // lifetime秒後にこのオブジェクトを破棄する
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (gameState == "gameover") return;

        // 移動処理
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public static Vector3 GetSpawnPositionOutsideCamera(float offset)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return Vector3.zero;

        // カメラのスクリーン境界をワールド座標で取得
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // カメラの外に位置を設定
        float spawnX = Random.value > 0.5f ? screenTopRight.x + offset : screenBottomLeft.x - offset;
        float spawnY = Random.Range(screenBottomLeft.y, screenTopRight.y);

        return new Vector3(spawnX, spawnY, 0);
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
