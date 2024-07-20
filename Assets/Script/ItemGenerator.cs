using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ゲーム画面で上から落ちてくるもの（赤リンゴ，金リンゴ，バクダン）を生成するためのスクリプト
//
public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;
    public GameObject goldApplePrefab;
    float span = 1.0f;      // アイテムを生成する時間間隔
    float delta = 0;
    float ratio = 0.2f;     // バクダンを生成する確率
    float speed = -0.03f;   // アイテムの落下速度
    string currentMode = "normal";    // 現在のモード（通常、反転、ボーナス）
    int stageSize; // ステージのサイズ
    float duration = 0; // ステージに移行してからの経過時間

    //
    // インスタンス変数の初期値を変更する
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
        // ステージ移行からの経過時間が3秒未満ならば、アイテムを生成しない。
        this.duration += Time.deltaTime;
        if (this.duration < 3.0) return;
        
        // 経過時間を加算する。
        this.delta += Time.deltaTime;

        // 経過時間がspanを超えると0に戻す。
        if (this.delta > this.span)
        {
            this.delta = 0;
            
            // 次のアイテムを生成する。
            GameObject item;
            int dice = Random.Range(1, 21);


            // 1 通常モードまたは反転モードならば以下の処理を行う。
            if(this.currentMode=="normal" || this.currentMode == "reverse")
            {
                // 1.1 5% の確率で落とすオブジェクトを金りんごにする。
                if (dice == 20)
                    item = Instantiate(goldApplePrefab) as GameObject;

                // 1.2 ratioに示す確率で落とすオブジェクトをバクダンにする。
                else if (dice <= ratio*20)
                    item = Instantiate(bombPrefab) as GameObject;

                // 1.3 それ以外の場合は落とすオブジェクトを赤りんごにする。
                else
                    item = Instantiate(applePrefab) as GameObject;
            }

            // 2 ボーナスモードならば落とすオブジェクトを赤りんごとする。
            else
                item = Instantiate(applePrefab) as GameObject;

            // 落下位置をランダムに設定する。
            float x = Random.Range(0, this.stageSize);
            float z = Random.Range(0, this.stageSize);
            item.transform.position = new Vector3(x, 4, z);

            // 落下速度を設定する。
            // 金りんごなら速度を1.2倍にする
            if (item.gameObject.tag == "GoldApple")
                item.GetComponent<ItemController>().dropSpeed = this.speed * 1.2f;
            else
                item.GetComponent<ItemController>().dropSpeed = this.speed;
            
        }
    }
}