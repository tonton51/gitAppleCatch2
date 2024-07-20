using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//
// Game��ʗp�̊ēX�N���v�g�i3x3, 4x4, 5x5�̃V�[���ɋ��ʂ̏������L�q�j
//
public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    float time = 48.0f;  // �c�莞��
    int point = 0;      // ���_
    GameObject generator;
    protected string currentScene;  // ���݂̃X�e�[�W
    protected string unclearedScene;  // �N���A���Ă��Ȃ��X�e�[�W

    GameObject modeText; // ���݂̃��[�h
    string currentMode = "normal";              // ���݂̃��[�h�i�ʏ�A���]�A�{�[�i�X�j
    int reverseModeCount = 0; // ���]���[�h�ɓ�������
    float remainingTime;    // �ʏ탂�[�h�ɖ߂������̎c�莞��

    GameObject comboText; //���݂̃R���{���e�L�X�g
    int combo = 0; //�R���{��
    int bonusModeCount = 0;// �{�[�i�X���[�h�ɓ�������

    protected int targetScore; // �e�V�[���i�X�e�[�W�T�C�Y3�`5�j�ɂ�����ڕW�X�R�A

    public AudioClip reverseSE; // ���]�V�[���ɓ���Ƃ���SE
    public AudioClip bonusSE; // �{�[�i�X�X�e�[�W�ɓ���Ƃ���SE
    public AudioClip normalSE;  // �m�[�}���X�e�[�W�ɓ���Ƃ���SE
    AudioSource aud; // SE��炷���߂ɕK�v
    GameObject countText;// �J�E���g�_�E���̃e�L�X�g
    int count = 3; //�J�E���g�_�E��

    // 
    // �Ԃ�񂲂��L���b�`�����ꍇ�̓_���v�Z
    // 
    public void GetApple()
    {
        // �ʏ탂�[�h�A�{�[�i�X���[�h�̂Ƃ���100�_���Z����
        if (currentMode == "normal"||currentMode=="bonus" && this.time < 45)
        {
            // �R���{����1���Z
            this.combo += 1; 

            //�R���{��5�ȉ��Ȃ�100�_�����Z����B
            if (this.combo <= 5)
                this.point += 100;

            //�R���{��5�ȏ�Ȃ猻�݂̓_����100+10�~n�_�����Z����B(n=�R���{��-5)
            else if (this.combo > 5)
                this.point +=  100 + (10 * (this.combo - 5));
        }

        // ���]���[�h�̂Ƃ��͓_����1/2�ɂ���
        else if (currentMode == "reverse")
            this.point /= 2;
    }

    // 
    // ����񂲂��L���b�`�����ꍇ�̓_���v�Z
    // 
    public void GetGoldApple()
    {
        // �ʏ탂�[�h�̂Ƃ���500�_���Z����
        if (currentMode == "normal" && this.time < 45)
        {
            //�R���{����1���Z����
            this.combo += 1; 

            // �R���{��5�ȉ��Ȃ猻�݂̓_����500�_���Z����B
            if (this.combo <= 5)
                this.point += 500;

            //�R���{��5�ȏ�Ȃ猻�݂̓_����500+10�~n�_�����Z����B(n=�R���{��-5)
            if (this.combo > 5)
                this.point +=  500 + (10 * (this.combo - 5));
        }

        // ���]���[�h�̎��͓_����1/2�ɂ���
        else if (currentMode == "reverse")
            this.point /= 2;
    }

    // 
    // �o�N�_�����L���b�`�����ꍇ�̓_���v�Z
    //
    public void GetBomb()
    {
        // �ʏ탂�[�h�̂Ƃ��͓_����1/2�ɂ���
        if (currentMode == "normal" && this.time < 45)
        {
            this.point /= 2;
            this.combo = 0;
        }

        // ���]���[�h�̎���100�_���Z����
        else if (currentMode == "reverse")
            this.point += 100;
    }

    //
    // �V�[�����ڍs�������̏����ݒ�
    //
    protected void Start()
    {
        // �e��I�u�W�F�N�g�̎Q�Ƃ�ݒ肷��
        this.generator = GameObject.Find("ItemGenerator");
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.modeText = GameObject.Find("Mode");
        this.comboText = GameObject.Find("Combo");
        this.aud = GetComponent<AudioSource>(); // SE��炷���߂ɕK�v
        this.countText = GameObject.Find("Count");
                                                  
        // ItemGenertor�̈ڍs����
        this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 0.2f, "normal", stageSize());
    }

    //
    // �V�[�����ł̃A�j���[�V�����̎��s
    //
    void Update()
    {
        //*******
        // ����������3�~3�A4�~4�A5�~5�̃Q�[����ʂɓ��������������s���Ȃ��B
        //*******

        // �c�莞�Ԃ��X�V����
        this.time -= Time.deltaTime;

        // 2 �ʏ�X�e�[�W�Ŕ��]�X�e�[�W�̉񐔂�0�Ȃ�΁A�w�肵���m���ňȉ��̏������s���B
        if (this.currentMode == "normal" && this.reverseModeCount == 0 && this.time<=35)
        {
            int dice = Random.Range(1, 11);
            int reverseProbability = 2; // �w�肵���m��
            if (dice <= reverseProbability)
            {
                // 2.1 ���]�X�e�[�W�ɑJ�ڂ���B
                this.currentMode = "reverse";
                this.remainingTime = this.time;    // �ʏ�X�e�[�W�ɖ߂������̎c�莞�Ԃ�ۑ�����B
                this.time = (float)10.0;          // �c�莞�Ԃ�10�b�ɂ���B

                // 2.2 ���]�X�e�[�W�̉񐔂�1���₷�B
                this.reverseModeCount++;

                // 2.3 ���]�X�e�[�W�̉���炷
                this.aud.PlayOneShot(this.reverseSE);

            }
        }
        // 1	�ʏ�X�e�[�W�ŃR���{����10�ɂȂ肩�{�[�i�X�X�e�[�W�̉񐔂�0�Ȃ�΁A�ȉ��̏������s���B+�R���{����10�ɂȂ�����
        if (this.currentMode == "normal" && this.bonusModeCount == 0 && this.combo == 10)
        {
            // 1.1 �{�[�i�X�X�e�[�W�ɑJ�ڂ���B
            this.currentMode = "bonus";
            this.remainingTime = this.time;// �ʏ�X�e�[�W�ɖ߂������̎c�莞�Ԃ�ۑ�����B
            this.time = (float)10.0;         // �c�莞�Ԃ�10�b�ɂ���B

            // 1.2 �{�[�i�X�X�e�[�W�̉񐔂�1���₷�B
            this.bonusModeCount++;

            // 1.3 �{�[�i�X�X�e�[�W�̉���炷
            this.aud.PlayOneShot(this.bonusSE);
        }

        // 3   �{�[�i�X�X�e�[�W�܂��͔��]�X�e�[�W���I�������ہA�ʏ�X�e�[�W�ɖ߂�B
        if ((this.time < 0) && ((this.currentMode == "reverse") || (this.currentMode == "bonus")))
        {
            this.currentMode = "normal";
            this.time = this.remainingTime;
            this.aud.PlayOneShot(this.normalSE);
        }

        // �c�莞�Ԃ��Ȃ��Ȃ�����V�[���I��
        if (this.time < 0 && this.currentMode == "normal")
        {
            //* �X�e�[�W�̏I����Ԃ�ۑ�����B
            //this.title.GetComponent<Director_Title>().setState(stageSize(), point, checkStageClear());
            Director_Title.setState(stageSize(), point, checkStageClear());

            // GameDirector�̈ڍs����
            this.time = 48.0f;
            this.point = 0;
            this.reverseModeCount = 0;
            this.combo = 0;
            this.bonusModeCount = 0;
            this.count = 3;

            // �X�e�[�W���N���A�������ۂ��ɂ�炸�A��UGameEnding�Ɉڍs����B
            SceneManager.LoadScene("GameEnding");

            // Update���\�b�h���I���
            return;
        }

        // 0�@�c�莞�Ԃ�48�`45�b�Ȃ�΁AUI�ŃJ�E���g�_�E����\������B
        else if (45 <= this.time && this.time < 48)
        {
            this.count = (int)(this.time - 45.0)+1;
            this.countText.GetComponent<Text>().text = this.count.ToString("F0");

        }

        // 1 �c�莞�Ԃ�45�`30�b�Ȃ�΁AUI��������1�b�Ԋu��ItemGenerator���Ăяo���B
        //   �܂��A�������x��0.03�A���Ƃ��I�u�W�F�N�g��20% �̊m���Ńo�N�_���ɂ���B
        else if (30 <= this.time && this.time < 45)
        {
            this.countText.GetComponent<Text>().text = " ";
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 0.2f, currentMode, stageSize());
        }

        // 2 �c�莞�Ԃ�30�`20�b�Ȃ�΁A0.8�b�Ԋu��ItemGenerator���Ăяo���B
        //   �܂��A�������x��0.04�A���Ƃ��I�u�W�F�N�g��40% �̊m���Ńo�N�_���ɂ���B
        else if (20 <= this.time && this.time < 30)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.04f, 0.4f, currentMode, stageSize());
        }

        // 3 �c�莞�Ԃ�20�`10�b�Ȃ�΁A0.5�b�Ԋu��ItemGenerator���Ăяo���B
        //   �܂��A�������x��0.05�A���Ƃ��I�u�W�F�N�g��60% �̊m���Ńo�N�_���ɂ���B
        else if (10 <= this.time && this.time < 20)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.5f, -0.05f, 0.6f, currentMode, stageSize());
        }

        // 4 �c�莞�Ԃ�10�`0�b�Ȃ�΁A0.7�b�Ԋu��ItemGenerator���Ăяo���B
        //   �܂��A�������x��0.04�A���Ƃ��I�u�W�F�N�g��30% �̊m���Ńo�N�_���ɂ���B
        else if (0 <= this.time && this.time < 10)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.7f, -0.04f, 0.3f, currentMode, stageSize());
        }

        // ��ʕ\�����X�V����B
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        this.pointText.GetComponent<Text>().text = this.point.ToString() + " point";
        this.modeText.GetComponent<Text>().text = this.currentMode.ToString();
        this.comboText.GetComponent<Text>().text = this.combo.ToString() + " combo";
    }

    //
    // ���݂̃V�[���ɂ�����X�e�[�W�̃T�C�Y��Ԃ�
    //
    public int stageSize()
    {
        switch (this.currentScene)
        {
            case "GameStage_3x3": return 3;
            case "GameStage_4x4": return 4;
            case "GameStage_5x5": return 5;
            default: return 0;  // �����l��Ԃ��B
        }
    }

    //
    // �V�[���̃N���A����
    //
    bool checkStageClear()
    {
        // ���݂̃X�e�[�W�ɂ�����ڕW���_���A���݂̓��_�������Ă���΃N���A�Ƃ���B
        if (this.targetScore <= this.point)
            return true;

        // �����łȂ���΃N���A���s�Ƃ���B
        else return false;
    }
}
