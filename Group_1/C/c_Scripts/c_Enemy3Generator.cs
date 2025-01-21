using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static c_GameManager;

public class c_Enemy3Generator : MonoBehaviour
{
    public GameObject enemyPrefab; // �G�l�~�[�̃v���n�u
    public float spawnInterval = 3f; // �G�l�~�[�̍ďo���Ԋu
    public float spawnOffset = 2f; // �J�����O�̐����I�t�Z�b�g

    public static string gameState;

    //private bool isGameOver = false; // �Q�[���I�[�o�[�t���O
    private float timer;

    void Update()
    {
        // �Q�[���I�[�o�[�Ȃ珈�����~
        if (gameState == "gameover") return;
        if (c_GameManager.CurrentState == GameState.GameOver) return;
        if (c_GameManager.CurrentState == GameState.GameClear) return;

        // �^�C�}�[�𑝉�
        timer += Time.deltaTime;

        // ���Ԋu�ŃG�l�~�[�𐶐�
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // �^�C�}�[�����Z�b�g
        }
    }

    void SpawnEnemy()
    {
        if (gameState == "gameover") return;

        Debug.Log("Sporn");

        // �G�l�~�[�̃X�|�[���ʒu���擾
        Vector3 spawnPosition = c_Enemy3Controller.GetSpawnPositionOutsideCamera(spawnOffset);

        // �G�l�~�[�𐶐�
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

   

}

