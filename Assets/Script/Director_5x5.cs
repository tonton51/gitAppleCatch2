using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 5x5��GameStage�p�̊ēX�N���v�g
//
public class Director_5x5 : GameDirector
{
    // Start is called before the first frame update
    void Start()
    {
        base.currentScene = "GameStage_5x5";  // ���݂̃X�e�[�W
        base.unclearedScene = "GameStage_5x5";  // �N���A���Ă��Ȃ��X�e�[�W
        base.targetScore = 500;                // �ڕW���_
        
        base.Start();
    }

}
