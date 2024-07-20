using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 4x４のGameStage用の監督スクリプト
//
public class Director_4x4 : GameDirector
{
    // Start is called before the first frame update
    void Start()
    {
        base.currentScene = "GameStage_4x4";  // 現在のステージ
        base.unclearedScene = "GameStage_4x4";  // クリアしていないステージ
        base.targetScore = 400;                // 目標得点

        base.Start();
    }

}
