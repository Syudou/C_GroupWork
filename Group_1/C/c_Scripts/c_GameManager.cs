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

    public GameObject inputPanel;

    public Button RetryButton;           //リトライボタン
    public Button GameSelectButton;      //ゲームセレクトボタン

    public float countdownTime = 60f; // カウントダウンの時間

    //public static string gameState;
    public static GameState CurrentState = GameState.Playing;

    public float delayTime = 2f;

    public enum GameState
    {
        Playing,
        GameOver,
        GameClear
    }



    void Start()
    {
        CurrentState = GameState.Playing;

        // ボタンを非表示にする
        RetryButton.gameObject.SetActive(false);
        GameSelectButton.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);

        inputPanel.SetActive(true);

        StartCoroutine(CountdownRoutine());

        StartCoroutine(HideTextWithDelay());

        
    }

    void Update()
    {
        // 現在のゲーム状態をチェック
        if (CurrentState == GameState.GameOver)
        {
            Debug.Log("ゲームオーバーです！");
            // ゲームオーバー処理
        }
        else if (CurrentState == GameState.GameClear)
        {
            Debug.Log("ゲームクリアです！");
            // ゲームクリア処理
        }
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
            if (CurrentState == GameState.GameOver)
            {
                GameOver();
                yield break;
            }

            // 残り時間を更新してUIに表示
            TimeText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

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

    void GameOver()
    {
        // 「ゲームオーバー」を表示
        GameOverText.gameObject.SetActive(true);
        GameOverText.text = "Game Over!";

        // カウントダウンの表示を消す
        TimeText.text = "";

        // ボタンを表示
        RetryButton.gameObject.SetActive(true);
        GameSelectButton.gameObject.SetActive(true);

        //ゲームパッドを非表示
        inputPanel.SetActive(false);
    }

    void GameClear()
    {
        // 「ゲームクリア」を表示
        GameOverText.gameObject.SetActive(true);
        GameOverText.text = "Game Clear!";

        c_GameManager.CurrentState = GameState.GameClear;
        

        // カウントダウンの表示を消す
        TimeText.text = "";

        // ボタンを表示
        RetryButton.gameObject.SetActive(true);
        GameSelectButton.gameObject.SetActive(true);

        //ゲームパッドを非表示
        inputPanel.SetActive(false);
    }


    public void RestartGame()
    {
        // 現在のシーンを再読み込みする
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RetryButton.gameObject.SetActive(false);
        GameSelectButton.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
    }

    public void GameSelectGame()
    {
        // 現在のシーンを再読み込みする
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
