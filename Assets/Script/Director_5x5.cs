using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 5x5のGameStage用の監督スクリプト
//
public class Director_5x5 : GameDirector
{
    // Start is called before the first frame update
    void Start()
    {
        base.currentScene = "GameStage_5x5";  // 現在のステージ
        base.unclearedScene = "GameStage_5x5";  // クリアしていないステージ
        base.targetScore = 500;                // 目標得点
        
        base.Start();
    }

}
