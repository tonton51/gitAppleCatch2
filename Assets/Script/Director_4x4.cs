using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 4x�S��GameStage�p�̊ēX�N���v�g
//
public class Director_4x4 : GameDirector
{
    // Start is called before the first frame update
    void Start()
    {
        base.currentScene = "GameStage_4x4";  // ���݂̃X�e�[�W
        base.unclearedScene = "GameStage_4x4";  // �N���A���Ă��Ȃ��X�e�[�W
        base.targetScore = 400;                // �ڕW���_

        base.Start();
    }

}
