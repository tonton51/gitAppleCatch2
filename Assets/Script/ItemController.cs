using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ゲーム画面で上から落ちてくるもの（赤リンゴ，金リンゴ，バクダン）を動かすためのスクリプト
//
public class ItemController : MonoBehaviour
{

    public float dropSpeed = -0.03f;

    void Update()
    {
        transform.Translate(0, this.dropSpeed, 0);
        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }
}