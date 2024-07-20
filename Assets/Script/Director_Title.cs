using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene���g�����߂ɕK�v!!

//
// GameTitle��ʗp�̊ēX�N���v�g
//
public class Director_Title : MonoBehaviour
{
    // ���O�̃X�e�[�W�̏I�����
    protected static int stageSize;     // �X�e�[�W�̃T�C�Y
    protected static int score;         // ���_
    protected static bool clearFlag;    // �N���A�������ۂ�

    //
    // �V�[����J�ڂ���
    //
    void Update()
    {
        // ��ʂ��N���b�N������3x3�Ɉړ�
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameStage_3x3");
        }
    }

    //
    // �ێ������I����Ԃ��X�V���郁�\�b�h
    //
    public static void setState(int stage_size, int _score, bool clear_flag)
    {
        stageSize = stage_size;
        score = _score;
        clearFlag = clear_flag;
    }

    //
    // �ێ������I����Ԃ�ǂݍ��ރ��\�b�h
    //
    public static int getStageSize() { return stageSize; }
    public static int getScore()     { return score; }
    public static bool getClearFlag() { return clearFlag ; }
}
