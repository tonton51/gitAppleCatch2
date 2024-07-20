using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadSceneを使うために必要!!

//
// GameTitle画面用の監督スクリプト
//
public class Director_Title : MonoBehaviour
{
    // 直前のステージの終了状態
    protected static int stageSize;     // ステージのサイズ
    protected static int score;         // 得点
    protected static bool clearFlag;    // クリアしたか否か

    //
    // シーンを遷移する
    //
    void Update()
    {
        // 画面がクリックされると3x3に移動
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameStage_3x3");
        }
    }

    //
    // 保持した終了状態を更新するメソッド
    //
    public static void setState(int stage_size, int _score, bool clear_flag)
    {
        stageSize = stage_size;
        score = _score;
        clearFlag = clear_flag;
    }

    //
    // 保持した終了状態を読み込むメソッド
    //
    public static int getStageSize() { return stageSize; }
    public static int getScore()     { return score; }
    public static bool getClearFlag() { return clearFlag ; }
}
