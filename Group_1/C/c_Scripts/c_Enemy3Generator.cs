using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static c_GameManager;

public class c_Enemy3Generator : MonoBehaviour
{
    public GameObject enemyPrefab; // エネミーのプレハブ
    public float spawnInterval = 3f; // エネミーの再出現間隔
    public float spawnOffset = 2f; // カメラ外の生成オフセット

    public static string gameState;

    //private bool isGameOver = false; // ゲームオーバーフラグ
    private float timer;

    void Update()
    {
        // ゲームオーバーなら処理を停止
        if (gameState == "gameover") return;
        if (c_GameManager.CurrentState == GameState.GameOver) return;
        if (c_GameManager.CurrentState == GameState.GameClear) return;

        // タイマーを増加
        timer += Time.deltaTime;

        // 一定間隔でエネミーを生成
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // タイマーをリセット
        }
    }

    void SpawnEnemy()
    {
        if (gameState == "gameover") return;

        Debug.Log("Sporn");

        // エネミーのスポーン位置を取得
        Vector3 spawnPosition = c_Enemy3Controller.GetSpawnPositionOutsideCamera(spawnOffset);

        // エネミーを生成
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

   

}

