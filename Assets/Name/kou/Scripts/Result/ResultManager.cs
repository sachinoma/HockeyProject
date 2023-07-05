using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ResultManager : MonoBehaviour
{
    private PlayerConfigurationManager playerConfigurationManager;

    [SerializeField]
    GameObject parent;

    [SerializeField]
    PlayerInput[] playerInputs;

    [SerializeField]
    GameObject menuItem;

    void Start()
    {
        playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        parent = playerConfigurationManager.transform.gameObject;
        foreach(MultiplayerEventSystem eventSystem in FindObjectsOfType<MultiplayerEventSystem>()) { eventSystem.SetSelectedGameObject(menuItem); }
    }

    //�Đ폈��
    public void OneMore()
    {
        ActivePlayerConfiguration(parent);
        SceneManager.LoadScene("MainGame");
    }

    //Title�ɖ߂�
    public void ToTitle()
    {
        DestroyPlayerConfiguration();
        ListEmpty();
        DestroyPlayerConfigurationManager();
        SceneManager.LoadScene("Title");
    }

    //PlayerConfiguration���A�N�e�B�u�ɂ���
    void ActivePlayerConfiguration(GameObject obj)
    {
        int maxPlayer = playerConfigurationManager.GetMaxPlayer();
        GameObject[] child = new GameObject[maxPlayer];

        //�q�I�u�W�F�N�g��PlayerConfiguration�����Ȃ��̂ŁAchild�������ł���
        for(int i = 0; i < maxPlayer; ++i)
        {
            child[i] = obj.transform.GetChild(i).gameObject;
            child[i].GetComponent<PlayerInput>().ActivateInput();
        }

        //���X�͏��Ԃ���Ȃ�child����+�X�N���v�g�����ł��������Ǐ�肭�����Ȃ��B
        /*
        Transform children = obj.GetComponentInChildren<Transform>();
        //�q�v�f�����Ȃ���ΏI��
        if(children.childCount == 0)
        {
            return;
        }
        foreach(Transform ob in children)
        {
            if(ob.gameObject.tag == ("PlayerConfiguration"))
            {
                Debug.Log("FindTag");
                ob.gameObject.SetActive(false);
            }
            GetChildren(ob.gameObject);
        }
        */
    }

    public void DestroyPlayerConfiguration()
    {
        //PlayerConfiguration�^�O������GameObject��z��Ŏ擾�����X�g�֕ϊ�
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("PlayerConfiguration").ToList();
        //�擾����GameObject���폜
        gameObjects.ForEach(gameObj => Destroy(gameObj));
    }
    public void DestroyPlayerConfigurationManager()
    {
        //PlayerConfigurationManager�^�O������GameObject��z��Ŏ擾�����X�g�֕ϊ�
        GameObject gameObject = GameObject.Find("PlayerConfigurationManager");
        //�擾����GameObject���폜
        Destroy(gameObject);
    }

    //playerConfigs���i�[���Ă��郊�X�g����ɂ���
    public void ListEmpty()
    {
        playerConfigurationManager.ListEmpty();
    }

   
}