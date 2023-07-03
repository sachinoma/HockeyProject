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

    //再戦処理
    public void OneMore()
    {
        ActivePlayerConfiguration(parent);
        SceneManager.LoadScene("MainGame");
    }

    //Titleに戻る
    public void ToTitle()
    {
        DestroyPlayerConfiguration();
        ListEmpty();
        DestroyPlayerConfigurationManager();
        SceneManager.LoadScene("Title");
    }

    //PlayerConfigurationをアクティブにする
    void ActivePlayerConfiguration(GameObject obj)
    {
        int maxPlayer = playerConfigurationManager.GetMaxPlayer();
        GameObject[] child = new GameObject[maxPlayer];

        //子オブジェクトはPlayerConfigurationしかないので、child順検索でする
        for(int i = 0; i < maxPlayer; ++i)
        {
            child[i] = obj.transform.GetChild(i).gameObject;
            child[i].GetComponent<PlayerInput>().ActivateInput();
        }

        //元々は順番じゃなくchild検索+スクリプト検索でしたいけど上手くいかない。
        /*
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
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
        //PlayerConfigurationタグをつけたGameObjectを配列で取得しリストへ変換
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("PlayerConfiguration").ToList();
        //取得したGameObjectを削除
        gameObjects.ForEach(gameObj => Destroy(gameObj));
    }
    public void DestroyPlayerConfigurationManager()
    {
        //PlayerConfigurationManagerタグをつけたGameObjectを配列で取得しリストへ変換
        GameObject gameObject = GameObject.Find("PlayerConfigurationManager");
        //取得したGameObjectを削除
        Destroy(gameObject);
    }

    //playerConfigsを格納しているリストを空にする
    public void ListEmpty()
    {
        playerConfigurationManager.ListEmpty();
    }

   
}
