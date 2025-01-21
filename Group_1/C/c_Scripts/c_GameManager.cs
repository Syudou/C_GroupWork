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

    public Button RetryButton;           //���g���C�{�^��
    public Button GameSelectButton;      //�Q�[���Z���N�g�{�^��

    public float countdownTime = 60f; // �J�E���g�_�E���̎���

    public static string gameState;

    public float delayTime = 2f;

    void Start()
    {
        // �{�^�����\���ɂ���
        RetryButton.gameObject.SetActive(false);
        GameSelectButton.gameObject.SetActive(false);
        
        StartCoroutine(CountdownRoutine());

        StartCoroutine(HideTextWithDelay());

        
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
            if (gameState == "gameover")
            {
                GameOver();
                yield break;
            }

            // �c�莞�Ԃ��X�V����UI�ɕ\��
            TimeText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString() + "�b";

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

    

}
