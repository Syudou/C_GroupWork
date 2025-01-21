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
        // �Q�[���I�[�o�[�Ȃ珈�����~
        if (gameState == "gameover") return;
        

        PlayerPos = Player.transform.position;//�v���C���[�̌��݈ʒu���擾
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos, speed * Time.deltaTime);//���݈ʒu����v���C���[�̈ʒu�Ɍ����Ĉړ�
            diff.x = PlayerPos.x - transform.position.x;//�v���C���[�ƓG�L������X���̈ʒu�֌W���擾����
            if (diff.x > 0)
            {
                //Player���G�L�����̉E���ɂ��鎞�E��������
                vector = new Vector3(0, -180, 0);
                transform.eulerAngles = vector;
            }
            if (diff.x < 0)
            {
                //Player���G�L�����̍����ɂ��鎞����������
                vector = new Vector3(0, 0, 0);
                transform.eulerAngles = vector;
            }
        
        
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
    

