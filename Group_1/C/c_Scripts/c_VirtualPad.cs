using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_VirtualPad : MonoBehaviour
{
    public float MaxLength = 70; //�^�u�������ő勗��
    public bool is4DPad = false; //�㉺���E�ɓ������t���O
    GameObject player; //�v���C���[�I�u�W�F�N�g
    Vector2 defPos; //�������O�̃p�b�h�̈ʒu
    Vector2 downPos; //�^�b�`�����ʒu


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //�^�u�̏������W(�e�I�u�W�F�N�g�ɑ΂��Ă̑��Έʒu 0�C0�C0)
        defPos = GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�^�b�v�����u�Ԃ̃C�x���g�p���\�b�h������
    public void PadDown()
    {
        //�^�b�v�����ꏊ�̃X�N���[����̍��W
        downPos = Input.mousePosition;
    }

    //�h���b�O�����u�Ԃ̃C�x���g�p���\�b�h������
    public void PadDrag()
    {
        //�h���b�O�����w�̃X�N���[����̍��W
        Vector2 mousePosition = Input.mousePosition;
        //�w�̃h���b�O�󋵂ɂ��킹�āA�p�b�h�̈ʒu�����߂�
        Vector2 newTabPos = mousePosition - downPos; //�ŏ��Ƀ^�b�v�����ʒu(downPos)����̍������ŏ��̈ʒu����ǂꂾ�����炵����

        //�����㉺���E���[�h��false�Ȃ��
        if (is4DPad == false)
        {
            newTabPos.y = 0; //�㉺�ɂ͓������Ȃ��̂�Y���Ɏw�肷��\��̒l�͂��炩����0�ɂ��Ă���
        }

        //�ړ��x�N�g�����v�Z����
        Vector2 axis = newTabPos.normalized; //���W�𐳋K����1�ɓ���

        //�p�b�h�̏����l�ƃY�����������̍������߂�
        float len = Vector2.Distance(defPos, newTabPos);

        //�ǂꂾ���w�����炵�Ă��p�b�h�̓����͍ő�l�܂łɍ����ւ�����
        //��axis�͕���������S���Ă���
        if (len > MaxLength)
        {
            //�l�̍����ւ�
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;
        }

        //���ۂ̃p�b�h���ʒu�����߂�
        GetComponent<RectTransform>().localPosition = newTabPos;

        //�A�����ăv���C���[�L�����𓮂���
        c_PlayerController plcnt = player.GetComponent<c_PlayerController>();
        plcnt.SetAxis(axis.x, axis.y);

    }

    //�w��b�������̃C�x���g
    public void PadUp()
    {
        //�^�u�̈ʒu�̏�����
        GetComponent<RectTransform>().localPosition = defPos;

        //�v���C���[�L�����N�^�[���~������
        c_PlayerController plcnt = player.GetComponent<c_PlayerController>();
        plcnt.SetAxis(0, 0);
    }

}
