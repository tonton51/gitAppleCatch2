using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ゲーム画面でのバスケットに対する操作を記述
// 
public class BasketController : MonoBehaviour
{

    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;
    int n;  // ステージのサイズ（3～5）

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        this.aud = GetComponent<AudioSource>();

        // ステージサイズを設定する。
        this.n = this.director.GetComponent<GameDirector>().stageSize();
    }

    void OnTriggerEnter(Collider other)
    {
        // 赤りんごをキャッチしたときの得点計算を行い効果音を出す。
        if (other.gameObject.tag == "Apple")
        {
            this.director.GetComponent<GameDirector>().GetApple();
            this.aud.PlayOneShot(this.appleSE);
        }

        // 金りんごをキャッチしたときの得点計算を行い効果音を出す。
        else if (other.gameObject.tag == "GoldApple")
        {
            this.director.GetComponent<GameDirector>().GetGoldApple();
            this.aud.PlayOneShot(this.appleSE);
        }

        // バクダンをキャッチしたときの得点計算を行い効果音を出す。
        else
        {
            this.director.GetComponent<GameDirector>().GetBomb();
            this.aud.PlayOneShot(this.bombSE);
        }

        // キャッチしたオブジェクトを消去する。
        Destroy(other.gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // クリックした位置に合わせてバスケットの位置を変更する。
            // n=3,4,5の場合も既存のものから修正の必要なし
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);

                transform.position = new Vector3(x, 0, z);
            }
        }
    }
}