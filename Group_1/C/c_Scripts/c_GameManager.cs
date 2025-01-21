using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class c_GameManager : MonoBehaviour
{
    public static c_GameManager Instance; // シングルトンインスタンス

    public TextMeshProUGUI GameStartText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI TimeText;

    public Button RetryButton;           //リトライボタン
    public Button GameSelectButton;      //ゲームセレクトボタン

    public float countdownTime = 60f; // カウントダウンの時間

    public static string gameState;

    public float delayTime = 2f;

    void Start()
    {
        // ボタンを非表示にする
        RetryButton.gameObject.SetActive(false);
        GameSelectButton.gameObject.SetActive(false);
        
        StartCoroutine(CountdownRoutine());

        StartCoroutine(HideTextWithDelay());

        
    }

    IEnumerator HideTextWithDelay()
    {
        // 指定した時間だけ待つ
        yield return new WaitForSeconds(delayTime);

        // ゲーム開始後、指定した時間が経過したらテキストを非表示に
        if (GameStartText != null)
        {
            GameStartText.enabled = false; // Textコンポーネントを非表示に
        }
    }

    IEnumerator CountdownRoutine()
    {
        float timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            // プレイヤーがゲームオーバーになった場合
            if (gameState == "gameover")
            {
                GameOver();
                yield break;
            }

            // 残り時間を更新してUIに表示
            TimeText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString() + "秒";

            // 残り時間を減らす
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        // 時間切れでゲームクリア
        GameClear();
    }


    private void Awake()
    {
        // シングルトンパターン
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

}
