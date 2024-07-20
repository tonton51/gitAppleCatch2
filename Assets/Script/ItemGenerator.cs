using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// �Q�[����ʂŏォ�痎���Ă�����́i�ԃ����S�C�������S�C�o�N�_���j�𐶐����邽�߂̃X�N���v�g
//
public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;
    public GameObject goldApplePrefab;
    float span = 1.0f;      // �A�C�e���𐶐����鎞�ԊԊu
    float delta = 0;
    float ratio = 0.2f;     // �o�N�_���𐶐�����m��
    float speed = -0.03f;   // �A�C�e���̗������x
    string currentMode = "normal";    // ���݂̃��[�h�i�ʏ�A���]�A�{�[�i�X�j
    int stageSize; // �X�e�[�W�̃T�C�Y
    float duration = 0; // �X�e�[�W�Ɉڍs���Ă���̌o�ߎ���

    //
    // �C���X�^���X�ϐ��̏����l��ύX����
    //
    public void SetParameter(float span, float speed, float ratio, string mode,int stageSize)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
        this.currentMode = mode;
        this.stageSize = stageSize;
    }

    void Update()
    {
        // �X�e�[�W�ڍs����̌o�ߎ��Ԃ�3�b�����Ȃ�΁A�A�C�e���𐶐����Ȃ��B
        this.duration += Time.deltaTime;
        if (this.duration < 3.0) return;
        
        // �o�ߎ��Ԃ����Z����B
        this.delta += Time.deltaTime;

        // �o�ߎ��Ԃ�span�𒴂����0�ɖ߂��B
        if (this.delta > this.span)
        {
            this.delta = 0;
            
            // ���̃A�C�e���𐶐�����B
            GameObject item;
            int dice = Random.Range(1, 21);


            // 1 �ʏ탂�[�h�܂��͔��]���[�h�Ȃ�Έȉ��̏������s���B
            if(this.currentMode=="normal" || this.currentMode == "reverse")
            {
                // 1.1 5% �̊m���ŗ��Ƃ��I�u�W�F�N�g������񂲂ɂ���B
                if (dice == 20)
                    item = Instantiate(goldApplePrefab) as GameObject;

                // 1.2 ratio�Ɏ����m���ŗ��Ƃ��I�u�W�F�N�g���o�N�_���ɂ���B
                else if (dice <= ratio*20)
                    item = Instantiate(bombPrefab) as GameObject;

                // 1.3 ����ȊO�̏ꍇ�͗��Ƃ��I�u�W�F�N�g��Ԃ�񂲂ɂ���B
                else
                    item = Instantiate(applePrefab) as GameObject;
            }

            // 2 �{�[�i�X���[�h�Ȃ�Η��Ƃ��I�u�W�F�N�g��Ԃ�񂲂Ƃ���B
            else
                item = Instantiate(applePrefab) as GameObject;

            // �����ʒu�������_���ɐݒ肷��B
            float x = Random.Range(0, this.stageSize);
            float z = Random.Range(0, this.stageSize);
            item.transform.position = new Vector3(x, 4, z);

            // �������x��ݒ肷��B
            // ����񂲂Ȃ瑬�x��1.2�{�ɂ���
            if (item.gameObject.tag == "GoldApple")
                item.GetComponent<ItemController>().dropSpeed = this.speed * 1.2f;
            else
                item.GetComponent<ItemController>().dropSpeed = this.speed;
            
        }
    }
}