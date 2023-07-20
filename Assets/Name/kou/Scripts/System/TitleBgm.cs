using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBgm : MonoBehaviour
{
    // �V���O���g��
    private static TitleBgm instance;

    void Awake()
    {

        // �V���O���g���̎���
        if(instance == null)
        {
            // ���g���C���X�^���X�Ƃ���
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �C���X�^���X���������݂��Ȃ��悤�ɁA���ɑ��݂��Ă����玩�g����������
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

}
