using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscExit : MonoBehaviour
{
    // �V���O���g��
    private static EscExit instance;

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
        // �G�X�P�[�v�L�[�ŃQ�[���I��
        if(Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
