using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManagerKou : MonoBehaviour
{
    private PlayerConfigurationManager playerConfigurationManager;

    private PlayerInputManager playerInputManager;

    [SerializeField]
    private Transform[] spawnPos;

    [SerializeField]
    private GameObject hockey;

    [SerializeField]
    float resetTime;

    [SerializeField]
    private int[] score;

    void Start()
    {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();
        playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        SpawnHockey();
    }

    //スコア加算
    public void ScorePlus(bool isLeft,int num)
    {
        if (isLeft) 
        {
            score[0] += num;
        }
        else
        {
            score[1] += num;
        }       
        Judge();
    }

    //Goalからこのメソッドを呼ぶ
    public void TriggerSpawnHockey()
    {
        SpawnHockey(resetTime);
    }

    //ホッケー生成の遅延バージョン
    public void SpawnHockey(float time)
    {
        Invoke("SpawnHockey", time);
    }

    //ホッケー生成
    public void SpawnHockey()
    {
        Instantiate(hockey, spawnPos[0].position, Quaternion.identity);
    }
    //デバッグ用の為にスコアが1以上の場合はシーン遷移
    private void Judge()
    {
        if (score[0] >= 1 || score[1] >= 1)
        {
            DestroyPlayer();
            DeActivePlayerConfiguration();
            Load();
        }
    }

    //リザルトに移行
    private void Load()
    {
        SceneManager.LoadScene("Result");
    }

    private void DestroyPlayer()
    {
        //PlayerタグをつけたGameObjectを配列で取得しリストへ変換
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
        //取得したGameObjectを削除
        gameObjects.ForEach(gameObj => Destroy(gameObj));
    }

    //PlayerConfigurationをデアクティブにする
    public void DeActivePlayerConfiguration()
    {
        //PlayerConfigurationタグをつけたGameObjectを配列で取得しリストへ変換
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("PlayerConfiguration").ToList();
        //公式のDeactivateInput()を使う
        gameObjects.ForEach(gameObj => gameObj.GetComponent<PlayerInput>().DeactivateInput());
    }
}
