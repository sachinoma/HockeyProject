using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    private PlayerConfigurationManager playerConfigurationManager;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip start;

    [SerializeField] AudioSource voiceAudioSource;
    [SerializeField] AudioClip[] chara1Voice;
    [SerializeField] AudioClip[] chara2Voice;

    [SerializeField] GameObject parent;

    [SerializeField] GameObject menuItem;

    [SerializeField] GameObject menuMain;

    [SerializeField] GameObject[] chara;

    [SerializeField] Animator cameraAnim;

    [SerializeField] Text winText;

    class Winner
    {
        private int winPlayer;
        private int winnerPrefabNum;

        public void SetWinPlayer(int num)
        {
            winPlayer = num;
        }
        public int GetWinPlayer()
        {
            return winPlayer;
        }
        public void SetWinPrefabNum(int num)
        {
            winnerPrefabNum = num;
        }
        public int GetWinPrefabNum()
        {
            return winnerPrefabNum;
        }
    }
    Winner winner;

    void Start()
    {
        audioSource.PlayOneShot(start, 1.0f);
        playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        parent = playerConfigurationManager.transform.gameObject;
        winner = new Winner();
        foreach (MultiplayerEventSystem eventSystem in FindObjectsOfType<MultiplayerEventSystem>()) { eventSystem.SetSelectedGameObject(menuItem); }
        SetPerformance();
        Invoke(nameof(MenuActive), 3.5f);
        Invoke(nameof(StartPlaySound), 2.3f);
    }

    public void StartPlaySound()
    {
        audioSource.Play();
    }

    private void MenuActive()
    {
        menuMain.SetActive(true);
    }

    private void SetPerformance()
    {
        winner.SetWinPlayer(playerConfigurationManager.GetWinner());
        winner.SetWinPrefabNum(playerConfigurationManager.GetWinnerPrefabNum());
        SetChara();
        SetWinText();
        SetCamera();
        Invoke(nameof(SetVoice), 0.5f);
    }

    private void SetChara()
    {
        chara[winner.GetWinPrefabNum()].SetActive(true);
    }

    private void SetWinText()
    {
        int winPlayerNum = winner.GetWinPlayer();
        if(winPlayerNum == 0) 
        {
            winText.text = "1P WIN";
        }
        else
        {
            winText.text = "2P WIN";
        }
    }

    private void SetVoice()
    {
        if(winner.GetWinPrefabNum() == 0)
        {
            voiceAudioSource.PlayOneShot(chara1Voice[Random.Range(0, chara1Voice.Length)], 1.0f);
        }
        else
        {
            voiceAudioSource.PlayOneShot(chara2Voice[Random.Range(0, chara2Voice.Length)], 1.0f);
        }
    }

    private void SetCamera()
    {
        cameraAnim.SetInteger("cameraWork", winner.GetWinPrefabNum());
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
