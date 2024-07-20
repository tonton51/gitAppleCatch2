using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 3x3のGameStage初期設定
//
public class Director_3x3 : GameDirector
{
    // Start is called before the first frame update
    void Start()
    {
        // 親クラスのインスタンス/変数を変更
        base.currentScene = "GameStage_3x3";  
        base.unclearedScene = "GameStage_3x3";  
        base.targetScore = 300;                

        // 親クラスのStartメソッド実行
        base.Start();
    }
    
}

