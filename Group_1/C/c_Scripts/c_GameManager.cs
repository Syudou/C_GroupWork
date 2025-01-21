using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class c_GameManager : MonoBehaviour
{
    public static c_GameManager Instance; // �V���O���g���C���X�^���X

    public TextMeshProUGUI GameStartText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI TimeText;

    public GameObject inputPanel;

    public Button RetryButton;           //���g���C�{�^��
    public Button GameSelectButton;      //�Q�[���Z���N�g�{�^��

    public float countdownTime = 60f; // �J�E���g�_�E���̎���

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

        // �{�^�����\���ɂ���
        RetryButton.gameObject.SetActive(false);
        GameSelectButton.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);

        inputPanel.SetActive(true);

        StartCoroutine(CountdownRoutine());

        StartCoroutine(HideTextWithDelay());

        
    }

    void Update()
    {
        // ���݂̃Q�[����Ԃ��`�F�b�N
        if (CurrentState == GameState.GameOver)
        {
            Debug.Log("�Q�[���I�[�o�[�ł��I");
            // �Q�[���I�[�o�[����
        }
        else if (CurrentState == GameState.GameClear)
        {
            Debug.Log("�Q�[���N���A�ł��I");
            // �Q�[���N���A����
        }
    }

    IEnumerator HideTextWithDelay()
    {
        // �w�肵�����Ԃ����҂�
        yield return new WaitForSeconds(delayTime);

        // �Q�[���J�n��A�w�肵�����Ԃ��o�߂�����e�L�X�g���\����
        if (GameStartText != null)
        {
            GameStartText.enabled = false; // Text�R���|�[�l���g���\����
        }
    }

    IEnumerator CountdownRoutine()
    {
        float timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            // �v���C���[���Q�[���I�[�o�[�ɂȂ����ꍇ
            if (CurrentState == GameState.GameOver)
            {
                GameOver();
                yield break;
            }

            // �c�莞�Ԃ��X�V����UI�ɕ\��
            TimeText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

            // �c�莞�Ԃ����炷
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        // ���Ԑ؂�ŃQ�[���N���A
        GameClear();
    }


    private void Awake()
    {
        // �V���O���g���p�^�[��
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
        // �u�Q�[���I�[�o�[�v��\��
        GameOverText.gameObject.SetActive(true);
        GameOverText.text = "Game Over!";

        // �J�E���g�_�E���̕\��������
        TimeText.text = "";

        // �{�^����\��
        RetryButton.gameObject.SetActive(true);
        GameSelectButton.gameObject.SetActive(true);

        //�Q�[���p�b�h���\��
        inputPanel.SetActive(false);
    }

    void GameClear()
    {
        // �u�Q�[���N���A�v��\��
        GameOverText.gameObject.SetActive(true);
        GameOverText.text = "Game Clear!";

        c_GameManager.CurrentState = GameState.GameClear;
        

        // �J�E���g�_�E���̕\��������
        TimeText.text = "";

        // �{�^����\��
        RetryButton.gameObject.SetActive(true);
        GameSelectButton.gameObject.SetActive(true);

        //�Q�[���p�b�h���\��
        inputPanel.SetActive(false);
    }


    public void RestartGame()
    {
        // ���݂̃V�[�����ēǂݍ��݂���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RetryButton.gameObject.SetActive(false);
        GameSelectButton.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
    }

    public void GameSelectGame()
    {
        // ���݂̃V�[�����ēǂݍ��݂���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
