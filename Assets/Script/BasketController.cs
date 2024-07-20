using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// �Q�[����ʂł̃o�X�P�b�g�ɑ΂��鑀����L�q
// 
public class BasketController : MonoBehaviour
{

    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;
    int n;  // �X�e�[�W�̃T�C�Y�i3�`5�j

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        this.aud = GetComponent<AudioSource>();

        // �X�e�[�W�T�C�Y��ݒ肷��B
        this.n = this.director.GetComponent<GameDirector>().stageSize();
    }

    void OnTriggerEnter(Collider other)
    {
        // �Ԃ�񂲂��L���b�`�����Ƃ��̓��_�v�Z���s�����ʉ����o���B
        if (other.gameObject.tag == "Apple")
        {
            this.director.GetComponent<GameDirector>().GetApple();
            this.aud.PlayOneShot(this.appleSE);
        }

        // ����񂲂��L���b�`�����Ƃ��̓��_�v�Z���s�����ʉ����o���B
        else if (other.gameObject.tag == "GoldApple")
        {
            this.director.GetComponent<GameDirector>().GetGoldApple();
            this.aud.PlayOneShot(this.appleSE);
        }

        // �o�N�_�����L���b�`�����Ƃ��̓��_�v�Z���s�����ʉ����o���B
        else
        {
            this.director.GetComponent<GameDirector>().GetBomb();
            this.aud.PlayOneShot(this.bombSE);
        }

        // �L���b�`�����I�u�W�F�N�g����������B
        Destroy(other.gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // �N���b�N�����ʒu�ɍ��킹�ăo�X�P�b�g�̈ʒu��ύX����B
            // n=3,4,5�̏ꍇ�������̂��̂���C���̕K�v�Ȃ�
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);

                transform.position = new Vector3(x, 0, z);
            }
        }
    }
}