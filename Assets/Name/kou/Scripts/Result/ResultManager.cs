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
        //playerConfigurationManager.GetComponent<PlayerInputManager>().enabled = false;
        parent = playerConfigurationManager.transform.gameObject;

        foreach(MultiplayerEventSystem eventSystem in FindObjectsOfType<MultiplayerEventSystem>()) { eventSystem.SetSelectedGameObject(menuItem); }
    }

    private void Update()
    {

    }

    public void OneMore()
    {
        GetChildren(parent);
        //playerConfigurationManager.GetComponent<PlayerInputManager>().enabled = true;
        SceneManager.LoadScene("MainGame");
    }

    public void ToTitle()
    {
        DestroyPlayerConfiguration();
        ListEmpty();
        DestroyPlayerConfigurationManager();
        SceneManager.LoadScene("Title");
    }


    public void ActivePlayerConfiguration()
    {
       
        playerInputs = parent.transform.GetComponentsInChildren<PlayerInput>();
        foreach(var item in playerInputs)
        {
            item.enabled = true;

            //タグが"Enemy"である子オブジェクトを削除する
            if (item.tag == "Enemy")
            {
                Destroy(item.gameObject);
            }
        }
    }

    void GetChildren(GameObject obj)
    {
        GameObject child0 = obj.transform.GetChild(0).gameObject;
        child0.GetComponent<PlayerInput>().ActivateInput();

        GameObject child1 = obj.transform.GetChild(1).gameObject;
        child1.GetComponent<PlayerInput>().ActivateInput();

        //Transform children = obj.GetComponentInChildren<Transform>();
        ////子要素がいなければ終了
        //if (children.childCount == 0)
        //{
        //    return;
        //}
        //foreach (Transform ob in children)
        //{
        //    if(ob.gameObject.tag ==("PlayerConfiguration"))
        //    {
        //        Debug.Log("FindTag");
        //        ob.gameObject.SetActive(false);
        //    }
        //    GetChildren(ob.gameObject);
        //}
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
        //PlayerConfigurationタグをつけたGameObjectを配列で取得しリストへ変換
        GameObject gameObject = GameObject.Find("PlayerConfigurationManager");
        //取得したGameObjectを削除
        Destroy(gameObject);
    }

    public void ListEmpty()
    {
        playerConfigurationManager.ListEmpty();
    }

   
}
