using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// �Q�[����ʂŏォ�痎���Ă�����́i�ԃ����S�C�������S�C�o�N�_���j�𓮂������߂̃X�N���v�g
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