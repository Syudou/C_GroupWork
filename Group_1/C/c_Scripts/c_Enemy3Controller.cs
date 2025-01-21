using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static c_GameManager;

public class c_Enemy3Controller : MonoBehaviour
{
    public float speed = 5f; // �G�l�~�[�̈ړ����x
    public float lifetime = 5f; // �G�l�~�[��������܂ł̎���

    public static string gameState;

    private Vector3 direction; // �ړ�����

    //private bool isGameOver = false;
    void Start()
    {
        if (gameState == "gameover") return;
        if (c_GameManager.CurrentState == GameState.GameOver) return;
        if (c_GameManager.CurrentState == GameState.GameClear) return;

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // �v���C���[�̕������v�Z
            direction = (player.transform.position - transform.position).normalized;
        }
        

        // lifetime�b��ɂ��̃I�u�W�F�N�g��j������
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (gameState == "gameover") return;

        // �ړ�����
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public static Vector3 GetSpawnPositionOutsideCamera(float offset)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return Vector3.zero;

        // �J�����̃X�N���[�����E�����[���h���W�Ŏ擾
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // �J�����̊O�Ɉʒu��ݒ�
        float spawnX = Random.value > 0.5f ? screenTopRight.x + offset : screenBottomLeft.x - offset;
        float spawnY = Random.Range(screenBottomLeft.y, screenTopRight.y);

        return new Vector3(spawnX, spawnY, 0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!"); // �R���\�[���Ƀ��b�Z�[�W��\��
        // �K�v�ɉ����āA�Q�[���I�[�o�[��ʂ�\������Ȃǂ̏�����ǉ�
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���݂̃V�[���������[�h
    }
    //public void StopMovement()
    //{
    //    isGameOver = true;
    //}
}
