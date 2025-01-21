using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static c_GameManager;

public class c_PlayerController : MonoBehaviour
{
    public float speed = 3.0f;          //�ړ��X�s�[�h
    int direction = 0;                  //�ړ�����
    float axisH;                        //����
    float axisV;                        //�c��
    public float angleZ = -90.0f;       //��]�p�x

    Rigidbody2D rbody;                  //Rigidbody2D
    Animator animator;                  //Animator
    private Vector2 movement;
    //�A�����W
    SpriteRenderer sprite; //SpriteRenderer���Q�Ɨ\��


    bool isMoving = false;              //�ړ����t���O

    //
    public static int hp = 1;       //
    public static string gameState; //
    bool inDamage = false;          //

    //���P���炐�Q�̊p�x��Ԃ� 
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
        rbody = GetComponent<Rigidbody2D>();    //Rigidbody2D�𓾂�
        animator = GetComponent<Animator>();    //Animator�𓾂�

        ////�A�����W
        //sprite = GetComponent<SpriteRenderer>(); //Player��SpriteRenderer���Q��


        //
        c_GameManager.CurrentState = GameState.Playing;
        gameState = "playing";

        //HP�X�V
        hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[����Ԃ�Playing�łȂ��ꍇ�͓������~�߂�
        if (c_GameManager.CurrentState != c_GameManager.GameState.Playing)
        {
            movement = Vector2.zero; // �ړ��x�N�g�������Z�b�g
            rbody.velocity = Vector2.zero;
            return; // Update�������I��
        }

        else
        {
            if (gameState != "playing" || inDamage)
            {
                return;
            }

            if (isMoving == false)
            {
                axisH = Input.GetAxisRaw("Horizontal");         //���E�L�[����
                axisV = Input.GetAxisRaw("Vertical");           //�㉺�L�[����
            }
            //�L�[���͂���ړ��p�x�����߂�
            Vector2 fromPt = transform.position;
            Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
            angleZ = GetAngle(fromPt, toPt);

            //�ړ��p�x��������Ă�������ƃA�j���[�V�����X�V
            int dir;
            if (angleZ >= -45 && angleZ < 45)
            {
                //�E����
                dir = 3;
            }
            else if (angleZ >= 45 && angleZ <= 135)
            {
                //�����
                dir = 2;
            }
            else if (angleZ >= -135 && angleZ <= -45)
            {
                //������
                dir = 0;
            }
            else
            {
                //������
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
            // �Q�[���N���A�܂��̓Q�[���I�[�o�[���ɑ��x�����S�Ƀ��Z�b�g
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


        //�ړ����x���X�V����
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    //�o�[�`�����p�b�h�p
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

            //�_���[�W���ł���Δ������Ȃ������G����
            if (!inDamage)
            {
                //�_���[�W���󂯂鎩�상�\�b�h
                GetDamage(collision.gameObject);
                

            }
        }
    }

    //�_���[�W�������郁�\�b�h
    void GetDamage(GameObject enemy)
    {
        if (gameState == "playing")
        {
            hp--; //HP�����炷
            

            if (hp > 0)
            {
                //�ړ�����U��~
                rbody.velocity = new Vector2(0, 0);
                //�Ԃ���������̂�������Ɣ��Ε���������o��
                Vector3 v = (transform.position - enemy.transform.position).normalized;

                //����o�������Ε����Ƀq�b�g�o�b�N������
                rbody.AddForce(new Vector2(v.x * 4, v.y * 4), ForceMode2D.Impulse);

                //�_���[�W�󂯒��̃t���O��ON
                inDamage = true;

                //���ԍ��Ń_���[�W�󂯒��̃t���O������
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
