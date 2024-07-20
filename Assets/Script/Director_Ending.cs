using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene���g�����߂ɕK�v!!
using UnityEngine.UI; // �X�R�A�C�N���A���ۂȂǃQ�[�����̃f�[�^��\������ۂɕK�v

//
// GameEnding��ʗp�̊ēX�N���v�g
//
public class Director_Ending : MonoBehaviour
{
    int stageSize; // �X�e�[�W�T�C�Y
    int score; // �_��
    bool clearFlag; // �N���A�������ۂ�
    GameObject clearText;
    GameObject scoreText;
    string clearresult; // �N���A����

    // Start is called before the first frame update
    void Start()
    {
        stageSize = Director_Title.getStageSize(); // ���O�̃X�e�[�W�T�C�Y
        score = Director_Title.getScore(); // ���O�̃X�e�[�W�̊l�������X�R�A
        clearFlag = Director_Title.getClearFlag();// ���O�̃X�e�[�W���N���A�������ۂ�

        // �X�R�A�C�N���A���ۂ̔���
        if (clearFlag == false)
            clearresult = "failure";

        else
            clearresult = "Clear!";

        this.clearText = GameObject.Find("Clear");// �N���A���ۂ̃e�L�X�g
        this.scoreText = GameObject.Find("Score");// �l�������X�R�A�̃e�L�X�g
    }

    //
    // ���O�̃X�e�[�W�ł̃N���A���ۂƃX�R�A�̕\��
    //
    void Update()
    {
        this.clearText.GetComponent<Text>().text = this.clearresult.ToString();
        this.scoreText.GetComponent<Text>().text = this.score.ToString() + "point";
    }

    // ���g���C�{�^�����������Ƃ��ɌĂ΂��C�x���g�n���h��
    public void PushRetryButton()
    {
        Debug.Log("Retry");
        // 3x3�̃X�e�[�W���N���A���Ă��Ȃ��Ƃ��C3x3�̃X�e�[�W�Ɉڍs����D
        if(stageSize == 3 && clearFlag == false)
            SceneManager.LoadScene("GameStage_3x3");

        // 4x4�̃X�e�[�W���N���A���Ă��Ȃ��Ƃ��C4x4�̃X�e�[�W�Ɉڍs����D
        else if (stageSize == 4 && clearFlag == false)
            SceneManager.LoadScene("GameStage_4x4");

        // 5x5�̃X�e�[�W�̂Ƃ��C�N���A�̐��ۂɊւ�炸�C5x5�̃X�e�[�W�Ɉڍs����D
        else if (stageSize == 5 )
            SceneManager.LoadScene("GameStage_5x5");
    }

    //
    // �u���̃X�e�[�W�ցv�{�^�������������̏���
    //
    public void PushNextButton()
    {
        Debug.Log("Next");
        // 3x3�̃X�e�[�W���N���A������C4x4�̃X�e�[�W�Ɉڍs����D
        if (stageSize == 3 && clearFlag == true)
            SceneManager.LoadScene("GameStage_4x4");

        // 4x4�̃X�e�[�W���N���A������C5x5�̃X�e�[�W�Ɉڍs����D
        else if (stageSize == 4 && clearFlag == true)
            SceneManager.LoadScene("GameStage_5x5");
    }

    // �I���{�^�����������Ƃ��ɌĂ΂��C�x���g�n���h��
    public void PushEndButton()
    {
        Debug.Log("End");
        // ������ʂɈڍs����D
        SceneManager.LoadScene("GameTitle");
    }
}
